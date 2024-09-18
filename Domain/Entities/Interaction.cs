using System;

namespace Domain.Entities
{
    public class Interaction
    {
        public Guid InteractionID { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public Guid ProjectID { get; set; } //FK
        public int InteractionType { get; set; } //FK
        public Project Project { get; set; }
        public InteractionType InteractionTypes { get; set; }
    }
}