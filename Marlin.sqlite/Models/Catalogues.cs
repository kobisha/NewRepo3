namespace Marlin.sqlite.Models
{
    public class Catalogues
    {
        public int ID { get; set; }
        public string AccountID { get; set; }
        public string ProductID { get; set; }
        public string SourceCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string Unit { get; set; }
        public string Status { get; set; }
        public DateTime LastChangeDate { get; set; }

    }
}
