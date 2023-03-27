namespace Marlin.sqlite.Models
{
    public class PriceList
    {
        public int ID { get; set; }
        public string AccountID { get; set; }
        public DateTime Date { get; set; }
        public string ProductID { get; set; }
        public string PriceType { get; set; }
        public double Unit { get; set; }
        public double Price { get; set; }

    }
}
