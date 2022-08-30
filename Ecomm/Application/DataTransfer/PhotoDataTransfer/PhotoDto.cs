﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.PhotoDataTransfer
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int? UserId { get; set; }

        public int? ProductId { get; set; }
    }
}
