using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class SubClass
    {
        public SubClass()
        {
            Thing = new HashSet<Thing>();
        }

        public int SubClassId { get; set; }
        public string Name { get; set; }
        public int? ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual ICollection<Thing> Thing { get; set; }
    }
}
