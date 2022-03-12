using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class FaqQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int FAQId { get; set; }
        public FAQ FAQ { get; set; }
    }
}
