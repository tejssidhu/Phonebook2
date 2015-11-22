using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.UI.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
    }
}