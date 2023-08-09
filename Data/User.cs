using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    internal class LoginAccountForm
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(30, ErrorMessage = "Mật khẩu dài tối thiểu 8 kí tự")]
        public string Password { get; set; }

    }

    internal class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Role { get; set; }
    }
}
