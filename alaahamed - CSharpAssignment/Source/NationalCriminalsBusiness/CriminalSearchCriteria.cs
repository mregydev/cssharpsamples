using NationalCriminalsBusiness.Enums;
using NationalCriminalsDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness
{
    [DataContract]
    public class CriminalSearchCriteria
    {
        #region Properties
        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public double MinAge { set; get; }

        [DataMember]
        public double MaxAge { set; get; }

        [DataMember]
        public double MinWeight { set; get; }

        [DataMember]
        public double MaxWeight { set; get; }

        [DataMember]
        public double MinHeight { set; get; }

        [DataMember]
        public double MaxHeight { set; get; }

        [DataMember]
        public bool IsMale { set; get; }

        [DataMember]
        public bool IgnoreGender { set; get; }

        [DataMember]
        public string Nationality { set; get; }

        [DataMember]
        public int MaxResultCount { set; get; }

        public bool IsValid
        {
            get
            {
                return MinAge >= 0 && MaxAge >= 0 && MaxAge >= MinAge && MinWeight >= 0 && MaxWeight >= 0 && MaxHeight >= MinWeight && MinHeight >= 0 && MaxHeight >= 0 && MaxHeight >= MinHeight;
            }
        }

        #endregion


    }
}
