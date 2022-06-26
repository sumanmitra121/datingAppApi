using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entities
{ 
    [Table("photos")]
    public class photo
    {
        public int Id { get; set; }
        public string url { get; set; }
        public bool isMain  { get; set; }
        public string publicId  { get; set; }

          public AppUser AppUser { get; set; }
          public int AppUserId { get; set; }



    }
}