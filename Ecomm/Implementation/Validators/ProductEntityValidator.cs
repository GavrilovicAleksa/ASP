using Application.DataTransfer.BrandDataTransfer;
using Application.DataTransfer.Product;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class ProductEntityValidator : AbstractValidator<ProductDto>
    {
        public ProductEntityValidator(Context context)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.BrandId).Must(x => context.Brands.Any(y => y.Id == x)).WithMessage("Provided Brand doesn't exist.");
        }
    }
}
