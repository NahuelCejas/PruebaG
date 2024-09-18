using System;

namespace Application.Response
{
    public class Interactions
    {
        public Guid Id { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public Guid ProjectId { get; set; }
        public GenericResponse InteractionType { get; set; }
    }
}