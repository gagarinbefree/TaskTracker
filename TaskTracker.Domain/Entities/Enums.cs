using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public enum TskStatus
    {
        New,
        InProgress,
        Done
    }

    public enum TskPriority
    {
        Low,
        Medium,
        High
    }

    public enum RelationshipType
    {
        RelatedTo
    }
}