using AutoMapper;
using FluentValidation;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Comments.Mapping;
using TechTask.Application.Comments.Models;
using TechTask.Application.Interfaces;

namespace TechTask.Application.Comments.Commands
{
    public class UpdateCommentCommand : IRequest<CommentDetailsDto>
    {
        public CommentForUpdateDto CommentForUpdateDto { get; set; }
    }

    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, CommentDetailsDto>
    {
        private readonly ICommentService _commentService;
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public UpdateCommentHandler(ICommentService commentService, ITasksService taskService,
            ITokenAuthenticationService authService, IMapper mapper)
        {
            _commentService = commentService;
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<CommentDetailsDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            //var validator = new UpdateCommentValidator();
            //validator.Validate(selector => request.CommentForUpdateDto);
            
            var taskFromDb = await _taskService.GetTaskWithoutEagerLoadingAsync(request.CommentForUpdateDto.TaskId);

            if (!_authService.UserRoleAdminOrUserIdMatches(taskFromDb.UserId))
                throw new AuthenticationException();

            var commentFromDb = await _commentService.GetCommentAsync(request.CommentForUpdateDto.CommentId);
            await _commentService.UpdateComment(commentFromDb, request.CommentForUpdateDto);

            return _mapper.Map<CommentDetailsDto>(commentFromDb);
        }
    }

    public class UpdateCommentValidator : AbstractValidator<CommentForUpdateDto>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please provide a valid description.");
        }
    }
}
