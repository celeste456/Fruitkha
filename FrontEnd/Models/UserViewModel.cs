using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FrontEnd.Models
{
    public class UserViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; } // Opcional para login
        public bool RememberLogin { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
