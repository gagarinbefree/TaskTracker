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
        public TskStatus Status { get; set; } = TskStatus.New;
        public TskPriority Priority { get; set; } = TskPriority.Medium;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Tsk> SubTasks { get; set; } = new List<Tsk>();
        public virtual ICollection<TskRelationship> RelatedTasks { get; set; } = new List<TskRelationship>();
    }
}
