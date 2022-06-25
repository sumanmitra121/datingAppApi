
namespace TestApi.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] passwordhash { get; set; }
        public byte[] passwordSalt { get; set; }
        
    }
}