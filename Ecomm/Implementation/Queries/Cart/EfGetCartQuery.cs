using Application.DataTransfer.CartDataTransfer;
using Application.Queries;
using Application.Queries.Cart;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Cart
{
    public class EfGetCartQuery : IGetCartQuery
    {
        private readonly Context _context;

        public EfGetCartQuery(Context context)
        {
            this._context = context;
        }

        public int Id => 18;

        public string Name => "Brand Search";

        public PagedResponse<GetCartDto> Execute(CartSearch search)
        {
            var query = _context.Carts.AsQueryable();

            if (search.UserId == null)
            {
                query = query.Where(x => x.UserId.Equals(search.UserId));
            } else
            {
                query = query.Where(x => x.Id.Equals(search.CartId));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<GetCartDto>
            {
                Page = search.Page,
                PerPage = search.PerPage,
                Count = query.Count(),
                Data = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetCartDto
                {
                    Id = x.Id,
                    User = x.User,
                    Products = x.CartItem.Select(x => x.Product)
                }).ToList()
            };

            return reponse;
        }
    }
}
