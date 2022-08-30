using Application;
using Application.DataTransfer.BrandDataTransfer;
using Application.DataTransfer.CartDataTransfer;
using Application.DataTransfer.Category;
using Application.Queries;
using Application.Queries.Brand;
using Application.Queries.Category;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Implementation.Queries.Category
{
    public class EfSearchCategoriesQuery : ISearchCategoriesQuery
    {
        private readonly Context _context;

        public EfSearchCategoriesQuery(Context context)
        {
            this._context = context;
        }

        public int Id => 18;

        public string Name => "Category Search";

        public PagedResponse<GetCategoryDto> Execute(CategorySearch search)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<GetCategoryDto>
            { 
                Page = search.Page,
                PerPage = search.PerPage,
                Count = query.Count(),
                Data = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return reponse;
        }
    }
}
