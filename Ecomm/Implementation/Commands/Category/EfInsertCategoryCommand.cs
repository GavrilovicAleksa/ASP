using Application.Commands.Category;
using Application.DataTransfer.Category;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Category
{
    public class EfInsertCategoryCommand : ICreateCategoryCommand
    {
        public int Id => 104;
        public string Name => "Create a Category";

        private readonly Context _context;

        private readonly CategoryEntityValidator _validator;

        public EfInsertCategoryCommand(Context context, CategoryEntityValidator validator)
        {
            this._validator = validator;
            this._context = context;
        } 

        public void Execute(CategoryDto request)
        {
            //this._validator.ValidateAndThrow(request);
             
            var category = new Domain.Category
            {
                Name = request.Name
            };   

            this._context.Categories.Add(category);

            this._context.SaveChanges();
        }
    }
}
