using System;
using System.Collections.Generic;
using TechTask.Persistence.Common;
using TechTask.Persistence.Models.Task;

namespace TechTask.Persistence.Models.Users
{
    public class Team : IBaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HoursOfWorkOnAllTasks { get; set; } 
        public List<Tasks> Tasks { get; set; }
        public List<User> Users { get; set; }   

    }
}
    