
using TestApi.Extensions;

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

        public DateTime dateOfbirth { get; set; }
        public string knownAs { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime last_active { get; set; } = DateTime.Now;

        public string gender { get; set; }
        public string introduction { get; set; }

        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<photo> photos { get; set; }

        // public int Getage(){
        //     return dateOfbirth.CalculateAge();
        // }
    }
}