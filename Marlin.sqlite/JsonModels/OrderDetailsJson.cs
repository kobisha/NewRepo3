namespace Marlin.sqlite.JsonModels
{
    public class OrderDetailsJson
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public double Unit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double ReservedQuantity { get; set; }
    }
}
