using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Project
    {
        public Guid ProjectID { get; set; } 
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ClientID { get; set; } //FK
        public int CampaignType { get; set; } //FK
        public Client Client { get; set; }  
        public CampaignType CampaignTypes { get; set; }
        public List<Interaction> Interactions { get; set; } = new List<Interaction>(); 
        public List<Task> Tasks { get; set; } = new List<Task>(); 
    }
}