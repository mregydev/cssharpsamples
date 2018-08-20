using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NationalCriminals.Models
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class UserModel
    {

        [Required(ErrorMessage = "You should enter valid UserName")]
        [Display(Name = "User name")]
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { set; get; }


        [Required(ErrorMessage = "You should enter valid Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        /// <summary>
        /// UserPassword
        /// </summary>
        public string Password { set; get; }



        [Required(ErrorMessage = "You should enter valid Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-Password")]
        [Compare("Password", ErrorMessage = "The password  do not match.")]
        /// <summary>
        /// UserPassword
        /// </summary>
        public string RePassword { set; get; }


        [Required(ErrorMessage = "You should enter valid Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email required.")]
        
        public string Email { set; get; }

    }
}