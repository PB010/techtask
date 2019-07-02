using TechTask.Persistence.Common;

namespace TechTask.Persistence.Models.Task
{
    public class TaskPriority : IBaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
