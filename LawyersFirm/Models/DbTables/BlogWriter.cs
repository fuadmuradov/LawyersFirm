using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class BlogWriter
    {
        public int Id { get; set; }

        public string Fullname { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
