using System.Collections.Generic;

namespace Application.Response
{
    public class ProjectDetails
    {
        public Project Data { get; set; }
        public List<Interactions> Interactions { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}