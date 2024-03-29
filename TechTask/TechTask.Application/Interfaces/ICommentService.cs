﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Comments.Mapping;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetAllCommentsAsync(int taskId);
        Task<int> AddNewCommentAsync(Comment comment);
        Task<int> RemoveCommentAsync(int commentId);
        Task<int> UpdateComment(Comment comment, CommentForUpdateDto dto);
    }
}