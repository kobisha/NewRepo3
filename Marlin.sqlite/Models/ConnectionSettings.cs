namespace Marlin.sqlite.Models
{
    public class ConnectionSettings
    {
        public int id { get; set; }
        public string AccountID { get; set; }
        public string ConnectedAccountID { get; set; }
        public bool AsBuyer { get; set; }
        public bool AsSupplier { get; set; }
        public string PriceTypes { get; set; }
        public string ConnectionStatus { get; set; }
    }
}
