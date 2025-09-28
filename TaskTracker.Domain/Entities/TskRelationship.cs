using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public class TskRelationship : BaseEntity
    {
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }

        public RelationshipTypeIdEnum TypeId { get; set; } = RelationshipTypeIdEnum.RelatedTo;

        public virtual Tsk? SourceTask { get; set; } = null;
        public virtual Tsk? TargetTask { get; set; } = null;
    }
}
