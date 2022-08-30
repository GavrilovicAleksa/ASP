﻿using Application.DataTransfer.Category;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public virtual IEnumerable<CategoryDto> Category { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
