using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XChess.Areas.LoginArea.Model
{
    public class RsPassCodeVM
    {
        [Required(ErrorMessage = "Vui lòng nhập mã xác minh.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Mã xác minh phải gồm đúng 6 số.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Mã xác minh không hợp lệ (chỉ gồm 6 chữ số).")]
        public string Code { get; set; }
    }
}