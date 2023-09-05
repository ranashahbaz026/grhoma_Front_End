using System.ComponentModel.DataAnnotations;

namespace SwissConfectionery.Models
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool Authenticate { get; set; }
    }
}
