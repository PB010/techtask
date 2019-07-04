using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;   
        private readonly ITokenAuthenticationService _authService;

        public GetAllUsersHandler(IUserService userService,
            IMapper mapper, ITokenAuthenticationService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<IEnumerable<UserDetailsDto>> Handle(GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException("You don't have permission to do that.");

            var users = await _userService.GetAllUsersAsync();

            return users.Select(_mapper.Map<UserDetailsDto>);
        }
    }
}
