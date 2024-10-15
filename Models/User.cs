namespace CRUD2.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string passwordhash { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
    }
}
