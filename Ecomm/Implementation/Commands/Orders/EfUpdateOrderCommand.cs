using Application.Commands.Order;
using Application.DataTransfer.Order;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Orders
{
    public class EfUpdateOrderCommand : IUpdateOrderCommand
    {
        public int Id => 116;

        public string Name => "Update an Order";

        private readonly Context _context;
        public EfUpdateOrderCommand(Context context)
        {
            _context = context;
        }

        public void Execute(OrderDto request)
        {

            var order = _context.Orders.Find(request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Domain.Order));
            }

     
            if (request.Products != null)
            {
                foreach (int ProductId in request.Products)
                {
                    var product = _context.Products.Find(ProductId);

                    if (product != null)
                    {
                        var productCategory = new ProductOrder
                        {
                            OrderId = order.Id,
                            ProductId = product.Id
                        }; 

                        order.ProductOrders.Add(productCategory);
                    }
                }
            } 

            if (request.DeliveredAt != null)
            {
                order.DeliveredAt = DateTime.Parse(request.DeliveredAt);
            }

            order.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
