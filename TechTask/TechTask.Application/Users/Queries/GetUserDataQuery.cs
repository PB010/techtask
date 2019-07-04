using AutoMapper;
using MediatR;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Queries
{
    public class GetUserDataQuery : IRequest<UserDetailsDto>
    {
        public Guid UserId { get; set; }    
    }

    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, UserDetailsDto>
    {
        private readonly IUserService _userService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetUserDataHandler(IUserService userService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<UserDetailsDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userService.GetUserAsync(request.UserId);
            var userDetails = _mapper.Map<UserDetailsDto>(userFromDb);


            if (_authService.UserRoleAdminOrEmailMatches(userFromDb.Email))
                return userDetails;

            throw new AuthenticationException("Unauthorized access.");
        }
    }
}
