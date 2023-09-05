using System.ComponentModel.DataAnnotations;

namespace SwissConfectionery.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not valid email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
