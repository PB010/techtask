using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Queries
{
    public class GetUserDataQuery : IRequest<UserDetailsDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, UserDetailsDto>
    {
        private readonly IUserService _userService;
    
        public GetUserDataHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDetailsDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            //var currentUser = jwt
            var userFromDb = await _userService.GetSingleUserAsync(request.Id);
            var userDetails = UserDetailsDto.ConvertToUserDetails(userFromDb);

            return userDetails;
        }
    }
    
    public class GetUserDataValidator : AbstractValidator<GetUserDataQuery>
    {
        public GetUserDataValidator(IUserService service)
        {
            RuleFor(x => x.Id)
                .Must(m => !service.UserExists(m))
                .WithErrorCode("404").WithMessage("This user doesn't exist.");
        }
    }
}
