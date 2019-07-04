using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Comments.Models;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Comments.Commands
{
    public class CreateNewCommentCommand : IRequest<CommentDetailsDto>
    {
        public CommentForCreationDto CommentForCreationDto { get; set; }    
    }

    public class CreateNewCommentHandler : IRequestHandler<CreateNewCommentCommand, CommentDetailsDto>
    {
        private readonly ICommentService _commentService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public CreateNewCommentHandler(ICommentService commentService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _commentService = commentService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<CommentDetailsDto> Handle(CreateNewCommentCommand request, CancellationToken cancellationToken)
        {
            request.CommentForCreationDto.UserId = new Guid(_authService.GetUserIdClaimValue());

            var commentToAdd = _mapper.Map<Comment>(request.CommentForCreationDto);
            await _commentService.AddNewCommentAsync(commentToAdd);

            return _mapper.Map<CommentDetailsDto>(commentToAdd);
        }
    }

    public class CreateNewCommentValidator : AbstractValidator<CommentForCreationDto>
    {
        public CreateNewCommentValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please provide a valid description.")
                .MaximumLength(150).WithMessage("Your comment is too long.");
        }
    }
}
