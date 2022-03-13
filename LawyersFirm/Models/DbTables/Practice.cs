using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Practice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Explonation { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public List<PriceToPractice> PriceToPractices { get; set; }

    }
}
