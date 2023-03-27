namespace Marlin.sqlite.Models
{
    public class OrderStatusHistory
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public DateTime Date { get; set; }
        public string StatusID { get; set; }
    }
}
