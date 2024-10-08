using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DATA.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public int ChucVuId { get; set; }
        public ChucVu? ChucVu { get; set; }
        public string? Address { get; set; }
    }
}
