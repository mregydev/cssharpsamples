using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NationalCriminals.Models
{
    public class CriminalModel
    {

        #region Properties


        [Required(ErrorMessage = "You should enter valid Criminal Name")]
        [Display(Name = "Criminal name")]
        public string Name
        {
            get;
            set;
        }



        [Required(ErrorMessage = "You should enter valid age")]
        [Display(Name = "Criminal Age")]
        [RegularExpression(@"[1-9]*\.?[0-9]+", ErrorMessage = "Age must be a Numbers only.")]
        /// <summary>
        /// Criminal age
        /// </summary>
        public double Age
        {
            set;
            get;
        }



        [Required(ErrorMessage = "You should enter valid sex type")]
        [Display(Name = "Is Male :")]
        public bool IsMale
        {
            set;
            get;
        }



        [Required(ErrorMessage = "You should enter valid nationality")]
        public string Nationality
        {
            set;
            get;
        }


        [Required(ErrorMessage = "You should enter valid weight")]
        [RegularExpression(@"[1-9]*\.?[0-9]+", ErrorMessage = "Weight must be a Numbers only.")]
        public double Weight
        {
            set;
            get;
        }


        [Required(ErrorMessage = "You should enter valid height")]
        [RegularExpression(@"[1-9]*\.?[0-9]+", ErrorMessage = "Height must be a Numbers only.")]
        public double Height
        {
            set;
            get;
        }

        #endregion

    }
}