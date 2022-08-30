using Application.Commands.Brand;
using Application.Commands.Photo;
using Application.Commands.Product;
using Application.DataTransfer.BrandDataTransfer;
using Application.DataTransfer.PhotoDataTransfer;
using Application.DataTransfer.Product;
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
    public class EfInsertProductCommand : ICreateProductCommand
    {
        private readonly Context _context;

        private readonly ProductEntityValidator _validator;

        public EfInsertProductCommand(Context context, ProductEntityValidator validator)
        {
            this._validator = validator;
            this._context = context;
        }
        public int Id => 110;

        public string Name => "Create a Brand";

        public void Execute(ProductDto request)
        {
            this._validator.ValidateAndThrow(request);

            var brand = _context.Brands.Find(request.BrandId);

            if (brand == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Brand));
            }

           
                var product = new Domain.Product
                {  
                    Name = request.Name,
                    Price = request.Price,
                    Brand_id = brand.Id,
                    CreatedAt = DateTime.Now
                };  

                this._context.Products.Add(product);

                this._context.SaveChanges();
          
        }
    }
}
