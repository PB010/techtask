using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;

namespace TechTask.Application.Comments.Commands
{
    public class DeleteCommentCommand : IRequest
    {
        public int CommentId { get; set; }  
    }   

    public class DeleteCommentHandler : AsyncRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentService _commentService;
        private readonly ITokenAuthenticationService _authService;

        public DeleteCommentHandler(ICommentService commentService, ITokenAuthenticationService authService)
        {
            _commentService = commentService;
            _authService = authService;
        }

        protected override async  Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            await _commentService.RemoveCommentAsync(request.CommentId);
        }
    }
}
