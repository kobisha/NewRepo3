namespace Marlin.sqlite.Models
{
    public class ProductCategories
    {
        public int Id { get; set; }
        public string AccountID { get; set; }
        public string GUID { get; set; }
        public string ParentFolder { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
