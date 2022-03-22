using LawyersFirm.Models.DbTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    public class AboutVM
    {
        public About About { get; set; }
        public Advantage Advantage { get; set; }
        public List<Attorney> Attorneys { get; set; }

    }
}
