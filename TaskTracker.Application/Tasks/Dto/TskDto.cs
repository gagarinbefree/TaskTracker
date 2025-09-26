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
        public Guid Id { get; set; }
        public Guid? ParentTaskId { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TskStatus Status { get; set; }
        public TskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<TskDto> SubTasks { get; set; } = new();
        public List<RelatedTskDto> RelatedTasks { get; set; } = new();
    }
}
