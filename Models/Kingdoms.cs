using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Kingdoms
    {
        public Kingdoms()
        {
            Class = new HashSet<Class>();
        }

        public int KingdomId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}
