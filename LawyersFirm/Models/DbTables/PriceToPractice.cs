using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class PriceToPractice
    {
        public int Id { get; set; }
        public int PriceId { get; set; }
        public int PracticeId { get; set; }
        public Price Price { get; set; }
        public Practice Practice { get; set; }
    }
}
