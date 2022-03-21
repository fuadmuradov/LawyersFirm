using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    public class UserEditVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage ="this incorrecy Email address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Does not match Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
