namespace PROJECT4.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
    }
    public enum Action
    {
        Error = 0, Success = 1, EmailExist = 2 ,NotRegistered = 3, NotFound=4
    }
}
