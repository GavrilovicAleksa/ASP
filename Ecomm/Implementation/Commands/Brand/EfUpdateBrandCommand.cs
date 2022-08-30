using Application.Commands.Brand;
using Application.DataTransfer.BrandDataTransfer;
using Application.Exceptions;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Brand
{
    public class EfUpdateBrandCommand : IUpdateBrandCommand
    {
        public int Id => 102;

        public string Name => "User Update";

        private readonly Context _context;

        private readonly BrandEntityValidator _validator;

        public EfUpdateBrandCommand(Context context, BrandEntityValidator validator)
        {
            _context = context;
            this._validator = validator;

        }

        public void Execute(BrandDto request)
        {
            //this._validator.ValidateAndThrow(request);

            var brand = _context.Brands.Find(request.Id);

            if (brand == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Brand));
            }

            if (request.UserId > 0)
            {
                var manager = _context.Users.Find(request.UserId);

                if (manager == null)
                {
                    throw new EntityNotFoundException(request.Id, typeof(Domain.User));
                }
                brand.Manager = manager;
            }
            if (request.Name != null)
            {
                brand.Name = request.Name;
            }
            if (request.Email != null) 
            {
                brand.Email = request.Email;
            }
            if (request.Description != null) 
            {
                brand.Description = request.Description;
            }
            if (request.Slogan != null) 
            {
                brand.Slogan = request.Slogan;
            }

            _context.SaveChanges();
        }
    }
}
