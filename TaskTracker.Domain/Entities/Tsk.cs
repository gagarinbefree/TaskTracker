using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public class Tsk : BaseEntity
    {
        public int? ParentTaskId { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public StatusIdEnum StatusId { get; set; } = StatusIdEnum.New;
        public PriorityIdEnum PriorityId { get; set; } = PriorityIdEnum.Low;

        public virtual ICollection<Tsk> SubTasks { get; set; } = new List<Tsk>();
        public virtual ICollection<TskRelationship> RelatedTasks { get; set; } = new List<TskRelationship>();
    }
}
