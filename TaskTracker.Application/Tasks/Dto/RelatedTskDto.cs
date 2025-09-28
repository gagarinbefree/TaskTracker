using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Dto
{
    public class RelatedTskDto
    {
        public Guid TaskId { get; set; }
        public RelationshipTypeIdEnum Type { get; set; }
    }
}
