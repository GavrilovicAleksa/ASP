﻿using Application.DataTransfer;
using Application.DataTransfer.BrandDataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Brand
{
    public interface IRemoveBrandCommand : ICommand<RemoveEntityDto>
    {
    }
}
