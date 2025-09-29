using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Dto
{
    public class TskRelationshipDto
    {
        public int Id { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
        public RelationshipTypeIdEnum TypeId { get; set; } = RelationshipTypeIdEnum.RelatedTo;
        public string TypeName { get; set; } = string.Empty;
    }
}
