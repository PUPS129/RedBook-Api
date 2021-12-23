using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Class
    {
        public Class()
        {
            SubClass = new HashSet<SubClass>();
        }

        public int ClassId { get; set; }
        public string Name { get; set; }
        public int? KingdomId { get; set; }

        public virtual Kingdoms Kingdom { get; set; }
        public virtual ICollection<SubClass> SubClass { get; set; }
    }
}
