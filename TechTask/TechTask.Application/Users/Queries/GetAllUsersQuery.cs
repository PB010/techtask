using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDetailsDto>>
    {
    }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDetailsDto>>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;

        public GetAllUsersHandler(IUserService userService, IHttpContextAccessor accessor)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<IEnumerable<UserDetailsDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException();

            var users = await _userService.GetAllUsersAsync();

            return users.Select(UserDetailsDto.ConvertToUserDetails);
        }
    }
}
