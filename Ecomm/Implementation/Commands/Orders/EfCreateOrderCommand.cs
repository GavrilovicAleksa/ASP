using Application.Commands.Order;
using Application.DataTransfer.CartDataTransfer;
using Application.DataTransfer.Order;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Order
{
    public class EfCreateOrderCommand : IAddOrderItemCommand
    {
        private readonly Context _context;
        public int Id => 4;

        public string Name => "Create order";

        public EfCreateOrderCommand(Context context)
        {
            this._context = context;
        }

        public void Execute(OrderDto request)
        {
            var user = _context.Users.Find(request.UserId);

            if(user == null)
            { 
                throw new EntityNotFoundException(request.Id, typeof(Domain.User));
            }  
             
            var order = new Domain.Order
            {
                PlacedAt = DateTime.Now,
                UserId = user.Id,
                AddressId = 1
            };

            _context.Orders.Add(order);

            _context.SaveChanges();
        }
    }
}
