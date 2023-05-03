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
        public DbSet<OrderHeaders> OrderHeaders => Set<OrderHeaders>();
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
        public DbSet<ConnectionSettings> ConnectionSettings => Set<ConnectionSettings>();
        public DbSet<ErrorCodes> ErrorCodes => Set<ErrorCodes>();
        public DbSet<ExchangeLog> ExchangeLog => Set<ExchangeLog>();
        public DbSet<Invoices> Invoices => Set<Invoices>();
        public DbSet<Messages> Messages => Set<Messages>();
        public DbSet<PositionName> PositionName => Set<PositionName>();
        public DbSet<Shops> Shops => Set<Shops>();
        public DbSet<UserPositions> UserPositions => Set<UserPositions>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<UserSettings> UserSettings => Set<UserSettings>();
        public DbSet<ProductsByCategories> ProductsByCategories => Set<ProductsByCategories>();
        public DbSet<Barcodes> Barcodes => Set<Barcodes>();
        public DbSet<ProductCategories> ProductCategories => Set<ProductCategories>();

        public DbSet<OrderHeaders> HeadersData { get; set; }
        public DbSet<OrderDetails> DetailsData { get; set; }
    }
}
