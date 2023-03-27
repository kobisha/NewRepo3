namespace Marlin.sqlite.Models
{
    public class InvoiceHeader
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public string InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public double Number { get; set; }
        public double Amount { get; set; }
        public string StatusID { get; set; }
        public string WaybillNumber { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
