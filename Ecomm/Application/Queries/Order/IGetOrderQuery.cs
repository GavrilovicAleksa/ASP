using Application.DataTransfer.Order;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Order
{
    public interface IGetOrderQuery : IQuery<OrderSearch, PagedResponse<GetOrderDto>>
    {
    }
}
