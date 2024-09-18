using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}