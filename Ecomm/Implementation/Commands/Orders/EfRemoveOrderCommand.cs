using Application.Commands.Order;
using Application.DataTransfer;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Order
{
    public class EfRemoveOrderCommand : IRemoveOrderCommand
    {
        public int Id => 5;

        public string Name => "Removes the order";

        private readonly Context _context;

        public EfRemoveOrderCommand(Context context)
        {
            _context = context;

        }

        public void Execute(RemoveEntityDto request)
        { 
            var order = _context.Orders.Find(request.Id);

            if (request.Hard == false)
            { 
                order.DeletedAt = new DateTime();
            }
            else
            { 
                _context.Orders.Remove(order);
            }

            _context.Orders.Remove(order);

            _context.SaveChanges();
        }
    }
}
