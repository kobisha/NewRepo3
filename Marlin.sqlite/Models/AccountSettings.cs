namespace Marlin.sqlite.Models
{
    public class AccountSettings
    {
        public int id { get; set; }
        public string AccountID { get; set; }
        public string BuyerWS { get; set; }
        public string SupplierWS { get; set; }
        public string BillingSettings { get; set; }
    }
}
