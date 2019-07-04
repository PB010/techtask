using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Comments.Models;
using TechTask.Application.Interfaces;

namespace TechTask.Application.Comments.Queries
{
    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDetailsDto>>
    {
        public int TeamId { get; set; } 
    }

    public class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDetailsDto>>
    {
        private readonly ICommentService _commentService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllCommentsHandler(ICommentService commentService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _commentService = commentService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDetailsDto>> Handle(GetAllCommentsQuery request, 
            CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdminOrTeamIdMatches(request.TeamId))
                throw new AuthenticationException();

            var commentsToReturn = await _commentService.GetAllCommentsAsync();

            return _mapper.Map<IEnumerable<CommentDetailsDto>>(commentsToReturn);
        }
    }
}
