using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Dto
{
    public class TskDto
    {
        public int Id { get; set; }
        public int? ParentTaskId { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public StatusIdEnum Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public PriorityIdEnum Priority { get; set; }
        public string PriorityName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<TskDto> SubTasks { get; set; } = new();
        public List<TskRelationshipDto> RelatedTasks { get; set; } = new();
    }
}
