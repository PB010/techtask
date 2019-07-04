﻿using System;
using TechTask.Persistence.Common;

namespace TechTask.Persistence.Models.Task
{
    public class Comment : IBaseClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TasksId { get; set; }
        public Guid UserId { get; set; }
    }
}
    