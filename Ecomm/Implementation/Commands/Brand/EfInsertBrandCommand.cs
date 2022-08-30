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
    public class EfInsertBrandCommand : ICreateBrandCommand
    {
        private readonly Context _context;

        private readonly BrandEntityValidator _validator;

        public EfInsertBrandCommand(Context context, BrandEntityValidator validator)
        {
            this._validator = validator;
            this._context = context;
        }
        public int Id => 101;

        public string Name => "Create a Brand";

        public void Execute(BrandDto request)
        {
            this._validator.ValidateAndThrow(request);

            var manager = _context.Users.Find(request.UserId);

            if (manager == null)
            {
                throw new EntityNotFoundException(request.UserId, typeof(Domain.User));
            }

            var brand = new Domain.Brand
            {
               Name = request.Name,
               Email = request.Email,
               Slogan = request.Slogan,
               Manager = manager
            }; 

            this._context.Brands.Add(brand);

            this._context.SaveChanges();
        }
    }
}
