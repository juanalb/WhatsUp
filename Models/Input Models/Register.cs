using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsUp.Models.Input_Models
{
    public class Register
    {
        [Required]
        [Display(Name = "Mobile Number")]
        public int MobileNumber { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Whatsup? Password and confirmation password no matchy.")]
        public string ConfirmPassword { get; set; }
        
    }
}