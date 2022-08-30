using Application.DataTransfer.BrandDataTransfer;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class BrandEntityValidator : AbstractValidator<BrandDto>
    {
        public BrandEntityValidator(Context context)
        {
            RuleFor(x => x.Description).NotEmpty(); 
            RuleFor(x => x.Name).NotEmpty().Must(x => !context.Brands.Any(y => y.Name == x)).WithMessage("Brand names are unique pleaase provide a valid one");
            RuleFor(x => x.UserId).NotEmpty().Must(x => context.Users.Any(y => y.Id == x)).WithMessage("Provided User doesn't exist.");
            RuleFor(x => x.Email).NotEmpty().Must(x => !context.Brands.Any(y => y.Email == x)).WithMessage("Brand emails are unique pleaase provide a valid one");
        }
    }
}
