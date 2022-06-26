
using System.ComponentModel.DataAnnotations;

namespace TestApi.DTOs
{
    public class registreduser
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
    }
}