using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Demo
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int Order { get; set; }
        public IFormFile MyProperty { get; set; }
    }
}
