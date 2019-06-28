using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Authentication;
using System.Security.Claims;
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
        private readonly ITokenAuthenticationService _authService;
        private readonly IHttpContextAccessor _accessor;

        public GetUserDataHandler(IUserService userService, ITokenAuthenticationService authService
        , IHttpContextAccessor accessor)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<UserDetailsDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userService.GetSingleUserAsync(request.Id);
            var userDetails = UserDetailsDto.ConvertToUserDetails(userFromDb);


            if (_accessor.HttpContext.User.IsInRole("Admin") ||
                _accessor.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Email &&
                                                         c.Value == userFromDb.Email))
                return userDetails;

            throw new AuthenticationException("Unauthorized access.");
        }
    }
    
    public class GetUserDataValidator : AbstractValidator<GetUserDataQuery>
    {
        public GetUserDataValidator(IUserService service, IHttpContextAccessor context)
        {
            RuleFor(x => x.Id)
                .Must(m => !service.UserExists(m))
                .WithMessage("This user doesn't exist.")
                .WithErrorCode("404");
        }
    }
}
