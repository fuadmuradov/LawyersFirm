using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }

    }
}
