using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string className { get; set; }
        public List<Case> Cases { get; set; }
    }
}
