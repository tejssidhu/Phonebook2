using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.UI.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Username")]
        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}