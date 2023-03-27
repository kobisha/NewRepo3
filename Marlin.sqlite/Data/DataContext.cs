using Marlin.sqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Data
{
    public class DataContext : DbContext
    {
        

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<OrderHeader> OrderHeaders => Set<OrderHeader>();
        public DbSet<Catalogues> Catalogues  => Set<Catalogues>();
        public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
        public DbSet<InvoiceHeader> InvoiceHeaders => Set<InvoiceHeader>();
        public DbSet<OrderDetails> OrderDetails => Set<OrderDetails>();
        public DbSet<OrderStatus> OrderStatus => Set<OrderStatus>();    
        public DbSet<OrderStatusHistory> OrderStatusHistory => Set<OrderStatusHistory>(); 
        public DbSet<PriceList> PriceList => Set<PriceList>();
    }
}
