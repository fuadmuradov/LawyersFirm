using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Price
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Package { get; set; }
      
        public int PriceContentId { get; set; }
        public PriceContent PriceContent { get; set; }
        public List<PriceToPractice> PriceToPractices { get; set; }

    }
}
