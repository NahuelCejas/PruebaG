using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
         public string Company { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}