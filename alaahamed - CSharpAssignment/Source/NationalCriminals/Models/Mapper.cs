using NationalCriminals.NationalCriminalsWcf;
using NationalCriminals.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalCriminals.Models
{
    /// <summary>
    /// Maps from Model to service entites and vice versa
    /// </summary>
    public class Mapper
    {
        public static UserProxy MapUserModelToUserProxy(UserModel user)
        {
            var userProxy = new UserProxy()
            {
                UserName = user.UserName,
                Password = SecurityHelper.CalculateMD5Hash(user.Password),
                Email = user.Email
            };

            return userProxy;
        }


        public static UserModel MapUserProxyToUserModel(UserProxy user)
        {
            var userProxy = new UserModel()
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email

            };

            return userProxy;
        }


        public static CriminalProxy MapCriminalModelToCriminalProxy(CriminalModel criminal)
        {
            var criminalProxy = new CriminalProxy()
            {
                Age = criminal.Age,
                Name = criminal.Name,
                Height = criminal.Height,
                Weight = criminal.Weight,
                Sex = criminal.IsMale ? GenderType.Male : GenderType.Female,
                Nationality = criminal.Nationality
            };

            return criminalProxy;
        }


        public static CriminalModel MapCriminalProxyToCriminalModel(CriminalProxy criminal)
        {
            var criminalModel = new CriminalModel()
            {
                Age = criminal.Age,
                Name = criminal.Name,
                Height = criminal.Height,
                Weight = criminal.Weight,
                IsMale = criminal.Sex == GenderType.Male,
                Nationality = criminal.Nationality
            };

            return criminalModel;
        }


        public static CriminalSearchCriteria MapCriminalSearchModelToCriminalSearchCriteria(CriminalsSearchModel searchModel)
        {
            var criminalSearchCrtieria = new CriminalSearchCriteria()
            {
                MaxAge = searchModel.MaxAge,
                MinAge = searchModel.MinAge,

                MaxWeight = searchModel.MaxWeight,
                MinWeight = searchModel.MinWeight,

                MaxHeight = searchModel.MaxHeight,
                MinHeight = searchModel.MinHeight,

                Name = searchModel.Name,
                Nationality = searchModel.Nationality,
                IsMale = searchModel.IsMale,
                IgnoreGender = searchModel.IsMale && searchModel.IsFemale,
                MaxResultCount = searchModel.MaxResultCount
            };

            return criminalSearchCrtieria;
        }


    }
}