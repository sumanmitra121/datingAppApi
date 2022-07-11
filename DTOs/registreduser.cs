
using System.ComponentModel.DataAnnotations;

namespace TestApi.DTOs
{
    public class registreduserDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 6)]
        public string password {get;set;}

        [Required]
        public string gender {get;set;}

        [Required]
        public string city {get;set;}

        [Required]
        public string country {get;set;}

        [Required]
        public string knownAs {get;set;}

         [Required]
        public DateTime dateOfbirth {get;set;}

        
    }
}