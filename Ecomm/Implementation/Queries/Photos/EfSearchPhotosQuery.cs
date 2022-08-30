using Application.DataTransfer.PhotoDataTransfer;
using Application.Queries;
using Application.Queries.Photo;
using Application.Searches;
using DataAccess;
using System.Linq;

namespace Implementation.Queries.Photo
{
    public class EfSearchPhotosQuery : ISearchPhotosQuery
    {
        private readonly Context _context;

        public EfSearchPhotosQuery(Context context)
        {
            this._context = context;
        }

        public int Id => 18;

        public string Name => "Brand Search";

        public PagedResponse<GetPhotoDto> Execute(PhotoSearch search)
        {
            var query = _context.Photos.AsQueryable();

            query = query.Where(x => x.User.Id.Equals(search.Id));

            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<GetPhotoDto>
            {
                Page = search.Page,
                PerPage = search.PerPage,
                Count = query.Count(),
                Data = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetPhotoDto
                {
                    Id = x.Id,
                    Url = x.Url,
                }).ToList()
            };

            return reponse;
        }
    }
}
