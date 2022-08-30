using Application.Commands.Category;
using Application.DataTransfer;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Category
{
    public class EfRemoveCategoryCommand : IRemoveCategoryCommand
    {
        public int Id => 105;
        public string Name => "Remove a brand";

        public EfRemoveCategoryCommand(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public void Execute(RemoveEntityDto request)
        {
            var category = _context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Category));
            }

            if (request.Hard == false)
            {
                category.DeletedAt = new DateTime();
            }
            else
            {
                _context.Categories.Remove(category);
            } 

            _context.SaveChanges();
        }
    }
}
