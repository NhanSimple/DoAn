using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XChess.Areas.UserArea.Model
{
    public class CreateVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirtName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}