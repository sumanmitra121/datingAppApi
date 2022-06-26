

namespace TestApi.DTOs
{
    public class MemeberDto
    {
         public int Id { get; set; }
        public string UserName { get; set; }
        public string  photoUrl{ get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public int age { get; set; }
        public string knownAs { get; set; }

        public DateTime created_at { get; set; }
        public DateTime last_active { get; set; }

        public string gender { get; set; }
        public string introduction { get; set; }

        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<photoDTO> photos { get; set; }
    }
}