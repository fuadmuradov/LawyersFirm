using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class OfficeImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public FirmInfo FirmInfo { get; set; }

    }
}
