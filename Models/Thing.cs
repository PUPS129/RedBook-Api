using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Thing
    {
        public int ThingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SubClassId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual SubClass SubClass { get; set; }
    }
}
