namespace Marlin.sqlite.Models
{
    public class Users
    {
        public int id { get; set; }
        public string AccountID { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string PositionInCompany { get; set; }
        public string Password { get; set; }
    }
}
