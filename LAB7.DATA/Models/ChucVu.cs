using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB7.DATA.Models
{
    public class ChucVu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên phải từ 3 đến 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tên chỉ được chứa chữ cái và khoảng trắng")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
