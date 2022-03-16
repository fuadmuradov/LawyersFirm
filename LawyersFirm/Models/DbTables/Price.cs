using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Price
    {
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Package { get; set; }
        [Required]
        public int PriceContentId { get; set; }
        [NotMapped]
        public List<int> PracticeIds { get; set; }
        public PriceContent PriceContent { get; set; }
        public List<PriceToPractice> PriceToPractices { get; set; }

    }
}
