using System;

namespace TechTask.Application.Comments.Models
{
    public class CommentDetailsDto
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public int TasksId { get; set; }
        public Guid UserId { get; set; }
    }
}
