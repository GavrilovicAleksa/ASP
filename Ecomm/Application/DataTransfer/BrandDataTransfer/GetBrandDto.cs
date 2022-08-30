﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.BrandDataTransfer
{
    public class GetBrandDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        public string Email { get; set; }

        public Domain.User Manager { get; set; }
    }
}