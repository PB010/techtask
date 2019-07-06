namespace TechTask.Application.Comments.Mapping
{
    public class CommentForUpdateDto
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; } 
    }
}
