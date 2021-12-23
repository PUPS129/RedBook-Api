using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Thing = new HashSet<Thing>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Thing> Thing { get; set; }
    }
}
