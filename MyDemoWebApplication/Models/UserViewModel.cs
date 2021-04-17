using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyDemoWebApplication.Models
{
    public class UserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName Required")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Invalid Length of UserName")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Invalid Length of Password")]
        public string Password { get; set; }

    }
}