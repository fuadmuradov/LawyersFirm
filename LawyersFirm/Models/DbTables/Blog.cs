using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Blog
    {
        public int Id { get; set; }

        public string SummaryTitle { get; set; }
        public string SummaryDesc { get; set; }
        public string HeaderTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int BlogWriterId { get; set; }
        public BlogWriter BlogWriter { get; set; }
        public int PracticeId { get; set; }
        public Practice Practice { get; set; }


    }
}
