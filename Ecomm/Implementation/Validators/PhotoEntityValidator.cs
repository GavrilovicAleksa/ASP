using Application.DataTransfer.PhotoDataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class PhotoEntityValidator : AbstractValidator<PhotoDto>
    {
        public PhotoEntityValidator(Context context)
        {
            RuleFor(x => x.ProductId).NotEmpty().Must(x => !context.Products.Any(y => y.Id == x)).WithMessage("Provided Product doesn't exist");
            RuleFor(x => x.UserId).NotEmpty().Must(x => context.Users.Any(y => y.Id == x)).WithMessage("Provided User doesn't exist.");
            RuleFor(x => x.Url).NotEmpty().Must(x => !context.Photos.Any(y => y.Url == x)).WithMessage("Photo Urls are unique pleaase provide a valid one");
        }
    }
}
