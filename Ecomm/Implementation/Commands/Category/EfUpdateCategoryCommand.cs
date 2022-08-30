using Application.Commands.Category;
using Application.DataTransfer.Category;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Category
{
    public class EfUpdateCategoryCommand : IUpdateCategoryCommand
    {
        public int Id => 106;
        public string Name => "Update a Category";

        private readonly Context _context;

        public EfUpdateCategoryCommand(Context context)
        {
            _context = context;

        }

        public void Execute(CategoryDto request)
        { 
            var category = _context.Categories.Find(request.Id);
                    
            if (category == null)       
            {  
                throw new EntityNotFoundException(request.Id, typeof(Domain.Category));
            }

            if (request.Name != null)
            {
                category.Name = Name;
            }

            _context.SaveChanges();
        }
    }
}
