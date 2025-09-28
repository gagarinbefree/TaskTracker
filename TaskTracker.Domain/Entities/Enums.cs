using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public enum StatusIdEnum
    {
        New = 1,
        InProgress = 2,
        Done = 3
    }

    public enum PriorityIdEnum
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public enum RelationshipTypeIdEnum
    {
        RelatedTo = 1
    }
}