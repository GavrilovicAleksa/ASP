using Application.Commands.Product;
using Application.DataTransfer.Product;
using Application.Exceptions;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Product
{
    public class EfUpdateProductCommand : IUpdateProductCommand
    {

        public int Id => 113;

        public string Name => "Update a Product";

        private readonly Context _context;
        public EfUpdateProductCommand(Context context)
        {
            _context = context;
        }

        public void Execute(ProductDto request)
        {

            var product = _context.Products.Find(request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Product));
            }

            var brand = _context.Brands.Find(request.BrandId);

            if(request.BrandId != null && brand != null)
            {
                product.Brand = brand;
            }

            if (request.Categories != null) 
            {  
                foreach (int CategoryId in request.Categories) 
                {
                    var category = _context.Categories.Find(CategoryId);

                    if (category != null) 
                    {
                        var productCategory = new ProductCategory
                        {
                            CategoryId = category.Id,
                            ProductId = product.Id
                        }; 

                        product.ProductCategory.Add(productCategory);
                    }
                } 
            }


            if (request.Name != null)
            {
                    product.Name = request.Name;
            } 

            if (request.Price != null)
            {
                product.Price = request.Price;
            }

            product.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
