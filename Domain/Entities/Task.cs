using System;

namespace Domain.Entities
{
    public class Task
    {
        public Guid TaskID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DueDate { get; set; }
        public int AssignedTo { get; set; } //FK
        public int Status { get; set; } //FK
        public Guid ProjectID { get; set; } //FK
        public TaskStatus TaskStatus { get; set; }
        public User User { get; set; } 
        public Project Project { get; set; }
    }
}