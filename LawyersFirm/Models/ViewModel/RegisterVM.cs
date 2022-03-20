using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [StringLength(maximumLength:25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        [EmailAddress(ErrorMessage ="Write Correct Email Address!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
