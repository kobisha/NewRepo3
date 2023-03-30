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
        public DbSet<AccessProfiles> AccessProfiles => Set<AccessProfiles>();
        public DbSet<AccessSettings> AccessSettings => Set<AccessSettings>();
        public DbSet<AccountSettings> AccountSettings => Set<AccountSettings>();
        public DbSet<ConnectionSettings> ConnectionSetting => Set<ConnectionSettings>();
        public DbSet<ErrorCodes> ErrorCodes => Set<ErrorCodes>();
        public DbSet<ExchangeLog> ExchangeLogs => Set<ExchangeLog>();
        public DbSet<Invoices> Invoices => Set<Invoices>();
        public DbSet<Messages> Messages => Set<Messages>();
        public DbSet<PositionName> PositionNames => Set<PositionName>();
        public DbSet<Shops> Shops => Set<Shops>();
        public DbSet<UserPositions> UserPositions => Set<UserPositions>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    }
}
