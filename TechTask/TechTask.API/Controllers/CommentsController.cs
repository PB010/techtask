﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Comments.Commands;
using TechTask.Application.Comments.Mapping;
using TechTask.Application.Comments.Models;
using TechTask.Application.Comments.Queries;
using TechTask.Application.Filters.Validators.CommentValidator;
using TechTask.Application.Filters.Validators.GeneralValidator;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/{teamId}/tasks/{taskId}/comments/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ServiceFilter(typeof(ValidateRouteAttributes))]
    public class CommentsController : BaseController
    {
        public CommentsController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        [ServiceFilter(typeof(ValidateAddComment))]
        public async Task<CommentDetailsDto> PostNewComment([FromRoute] int teamId,
            [FromRoute] int taskId, [FromBody] CommentForCreationDto dto)
        {
            dto.TasksId = taskId;
            dto.TeamId = teamId;

            return await _mediator.Send(new CreateNewCommentCommand {CommentForCreationDto = dto});
        }

        [HttpGet("{commentId}")]
        public async Task<CommentDetailsDto> GetCommentForTask([FromRoute] int teamId,
            [FromRoute] int taskId, [FromRoute] int commentId)
        {
            return await _mediator.Send(new GetCommentQuery {CommentId = commentId, TeamId = teamId});
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDetailsDto>> GetAllCommentsForTask([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new GetAllCommentsQuery {TeamId = teamId, TaskId = taskId});
        }

        [HttpDelete("{commentId}")]
        public async Task<Unit> DeleteComment([FromRoute] int teamId,
            [FromRoute] int taskId, [FromRoute] int commentId)
        {
            return await _mediator.Send(new DeleteCommentCommand {CommentId = commentId});
        }

        [HttpPut("{commentId}")]
        public async Task<CommentDetailsDto> UpdateComment([FromRoute] int teamId,
            [FromRoute] int taskId, [FromRoute] int commentId, [FromBody] CommentForUpdateDto dto)
        {
            dto.CommentId = commentId;
            dto.TaskId = taskId;

            return await _mediator.Send(new UpdateCommentCommand { CommentForUpdateDto = dto});
        }
    }
}
