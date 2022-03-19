using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="you should be use a Correct Email Address")]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
