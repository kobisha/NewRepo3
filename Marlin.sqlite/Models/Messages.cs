namespace Marlin.sqlite.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string MessageID { get; set; }
        public DateTime Date { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Type { get; set; }
        public string JSONBody { get; set; }

    }
}
