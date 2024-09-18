using System;

namespace Application.Response
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public GenericResponse Status { get; set; }
        public Users UserAssigned { get; set; }
    }
}