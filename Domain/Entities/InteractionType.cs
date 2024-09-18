using System.Collections.Generic;

namespace Domain.Entities
{
    public class InteractionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Interaction> Interactions {get; set; }
    }
}