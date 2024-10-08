using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB7.DATA.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên phải từ 3 đến 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tên chỉ được chứa chữ cái và khoảng trắng")]
        public string Name { get; set; }
        [Range(18, 100, ErrorMessage = "Tuổi phải từ 18 đến 100")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(\+?\d{1,3}?[-.\s]?)?(\(?\d{3}\)?[-.\s]?)?[\d\s]{7,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        public string? Address { get; set; }
        public int ChucVuId { get; set; }
        public ChucVu? ChucVu { get; set; }
    }
}
