using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; } = new HashSet<ProductCategory>();
    }
} 
