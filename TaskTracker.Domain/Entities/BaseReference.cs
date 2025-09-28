using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Entities
{
    public class BaseReference
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
