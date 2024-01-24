namespace ChatParty.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int Status { get; set; }

    }
}
 