namespace Marlin.sqlite.Models
{
    public class ExchangeLog
    {
        public int id { get; set; }
        public string TransactionID { get; set; }
        public DateTime Date { get; set; }
        public string MessageID { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
    }
}
