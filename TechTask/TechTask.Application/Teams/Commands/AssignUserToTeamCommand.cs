using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;
using TechTask.Persistence.Context;

namespace TechTask.Application.Teams.Commands
{
    public class AssignUserToTeamCommand : IRequest<TeamDetailsDto>
    {
        public IdAttributesDto IdAttributesDto { get; set; }
    }

    public class AssignUserToTeamHandler : IRequestHandler<AssignUserToTeamCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public AssignUserToTeamHandler(ITeamService teamService, IUserService userService,
            IHttpContextAccessor accessor, IMapper mapper)
        {
            _teamService = teamService;
            _userService = userService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<TeamDetailsDto> Handle(AssignUserToTeamCommand request, CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamWithEagerLoadingAsync(request.IdAttributesDto.Id);
            var userFromDb = await _userService.GetUserAsync(request.IdAttributesDto.UserId);

            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("You don't have permission to do that.");

            await _teamService.AssignUserToTeam(teamFromDb, userFromDb);

            var teamToReturn = _mapper.Map<TeamDetailsDto>(teamFromDb);

            return teamToReturn;
        }

        public class IdAttributesValidator : AbstractValidator<IdAttributesDto>
        {
            public IdAttributesValidator(AppDbContext context)
            {
                RuleFor(x => x.UserId).Must(m => context.Users.Any(u => u.Id == m))
                    .WithMessage("There is no such user in DB.")
                    .WithErrorCode("404");  

                RuleFor(x => x.UserId)
                    .Must(m => context.Teams.Include(t => t.Users)
                        .All(t => t.Users.All(u => u.Id != m)))
                    .WithMessage("This user is already a part of a team.")
                    .WithErrorCode("400");
            }
        }
    }
}
