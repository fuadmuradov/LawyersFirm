using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    [NotMapped]
    public class OfficeImageDescVM
    {
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
    }
}
