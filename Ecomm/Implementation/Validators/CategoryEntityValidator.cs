using Application.DataTransfer.Category;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{ 
    public class CategoryEntityValidator : AbstractValidator<CategoryDto>
    {
        public CategoryEntityValidator(Context context)
        {
            RuleFor(x => x.Name).NotEmpty().Must(x => !context.Categories.Any(y => y.Name == x)).WithMessage("Category names are unique pleaase provide a valid one");
            RuleFor(x => x.ParentId).NotEmpty().Must(x => context.Categories.Any(y => y.Id == x)).WithMessage("Provided Parent doesn't exist.");
        }
    } 
}
