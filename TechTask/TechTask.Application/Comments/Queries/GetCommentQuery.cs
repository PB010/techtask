using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Comments.Models;
using TechTask.Application.Interfaces;

namespace TechTask.Application.Comments.Queries
{
    public class GetCommentQuery : IRequest<CommentDetailsDto>
    {
        public int CommentId { get; set; }
        public int TeamId { get; set; } 
    }

    public class GetCommentHandler : IRequestHandler<GetCommentQuery, CommentDetailsDto>
    {
        private readonly ICommentService _commentService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetCommentHandler(ICommentService commentService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _commentService = commentService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<CommentDetailsDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdminOrTeamIdMatches(request.TeamId))
                throw new AuthenticationException();

            var commentFromDb = await _commentService.GetCommentAsync(request.CommentId);

            return _mapper.Map<CommentDetailsDto>(commentFromDb);
        }
    }
}
