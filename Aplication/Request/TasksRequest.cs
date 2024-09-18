using System;

namespace Application.Request
{
    public class TasksRequest
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public int User { get; set; }
        public int Status { get; set; }
    }
}