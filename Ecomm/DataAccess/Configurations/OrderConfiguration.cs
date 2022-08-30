using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        { 
            builder.Property(x => x.PlacedAt).IsRequired();   
            builder.HasOne(x => x.User).WithMany(u => u.Orders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction); 
            builder.HasOne(x => x.Address).WithMany(x => x.Orders).HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.ProductOrders).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Restrict);
        }    
    }
}
