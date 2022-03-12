using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class FirmInfo
    {
        public int Id { get; set; }
        [Required]
        public int Earned { get; set; }

        public List<InfoDesc> InfoDescs { get; set; }
        public List<OfficeImage> OfficeImages { get; set; }

    }
}
