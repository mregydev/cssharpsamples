using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NationalCriminals.Models
{
    public class CriminalsSearchModel
    {
        public string Name { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MinAge { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MaxAge { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MinWeight { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MaxWeight { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MinHeight { set; get; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Must be positive Numbers only.")]
        public double MaxHeight { set; get; }

        public bool IsMale { set; get; }

        public bool IsFemale { set; get; }

        public string Nationality { set; get; }

        public int MaxResultCount { set; get; }

    }
}