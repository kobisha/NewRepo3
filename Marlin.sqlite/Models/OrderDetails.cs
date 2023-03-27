namespace Marlin.sqlite.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public double Unit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double ReservedQuantity { get; set; }

    }
}
