using Application.DataTransfer.Category;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.Category;
using Application.Searches;
using DataAccess;

namespace Implementation.Queries.Category
{ 
    public class EfGetSingleCategoryQuery : IGetCategoryQuery
    {
        private readonly Context _context;

        public EfGetSingleCategoryQuery(Context context)
        {
            this._context = context;
        }
        public int Id => 27;

        public string Name => "Find a Category by its Id";

        public SingleResponse<GetCategoryDto> Execute(CategorySearch search)
        {
            var brand = _context.Brands.Find(search.Id);

            if (brand == null)
            {
                throw new EntityNotFoundException(search.Id, typeof(Domain.Brand));
            }

            var response = new SingleResponse<GetCategoryDto>
            {
                Data = new GetCategoryDto
                {
                    Id = brand.Id,
                    Name = brand.Name
                }
            };

            return response;
        }
    }
}
