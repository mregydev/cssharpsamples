using NationalCriminalsBusiness;
using NationalCriminalsBusiness.Business_Classes.Email_Business;
using NationalCriminalsBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace NationalCriminalsWcf
{
    public class NationalCriminals : INationalCriminalSvc
    {

        #region INationalCriminalSvc Implmentation

        /// <summary>
        /// Adds a new user to the database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(NationalCriminalsBusiness.Entities.UserProxy user)
        {
            return user.AddToDataBase();
        }

        /// <summary>
        /// Adds a new criminal to the database 
        /// </summary>
        /// <param name="criminal"></param>
        /// <returns></returns>
        public bool AddCriminal(NationalCriminalsBusiness.Entities.CriminalProxy criminal)
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            criminal.FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + ConfigurationManager.AppSettings["criminalsPdfFolder"]) + @"\" + criminal.Name + Guid.NewGuid() + ".pdf";
            return criminal.AddToDatabase();
        }


        /// <summary>
        /// Check that passed user is in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserProxy GetUserInstanceFromDb(UserProxy user)
        {
            return user.GetInstance();
        }

        /// <summary>
        /// Search for criminals based on passed searchCriteria
        /// This method starts a new thread
        /// calling SearchCriminals which in turn search for passed criminals and send themm email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public bool SearchCrininals(string email, CriminalSearchCriteria searchCriteria)
        {
            var parameters = new List<object> { searchCriteria, email };

            Thread thr = new Thread(SearchCriminals);
            thr.Start(parameters);


            return searchCriteria.IsValid;
        }

        #endregion

        #region Helpers

        private void SearchCriminals(object parameters)
        {
            var searchCriteria = (CriminalSearchCriteria)(parameters as List<object>)[0];

            var email = (parameters as List<object>)[1].ToString();
            //Get criminals that apply search criteria
            var criminals = CriminalProxy.GetCriminals(searchCriteria);

            if (criminals.Count > 0)
            {

                var criminalsToSend = new List<CriminalProxy>();

                //Iterate through them
                for (var i = 0; i < criminals.Count; ++i)
                {
                    //Add them to criminals list that will be send 
                    criminalsToSend.Add(criminals[i]);

                    //for each 10 criminals we send in a separate email
                    if (i > 0 && i % 10 == 0)
                    {
                        StartEmailThread(criminalsToSend, email);

                        criminals.Clear();
                    }
                }

                if (criminalsToSend.Any())
                {
                    StartEmailThread(criminalsToSend, email);

                }
            }
            else
            {
                EmailSender.SendEmail(email, "No criminals found", "Criminal List");
            }

        }

        /// <summary>
        /// Send passed criminals to passed mail 
        /// runs in a separate thread
        /// </summary>
        /// <param name="parameters"></param>
        private void SendCriminalsProfile(object parameters)
        {
            var criminals = (List<CriminalProxy>)(parameters as List<object>)[0];

            var email = (parameters as List<object>)[1].ToString();

            EmailSender.SendEmail(email, criminals);

        }




        /// <summary>
        /// Starts a new thread an call SendCriminalsProfile
        /// which in turn send passed criminals to passed mail
        /// </summary>
        /// <param name="criminalsToSend"></param>
        /// <param name="email"></param>
        private void StartEmailThread(List<CriminalProxy> criminalsToSend, string email)
        {
            var paramters = new List<object> { criminalsToSend, email };

            Thread thr = new Thread(SendCriminalsProfile);
            thr.Start(paramters);

        }
        #endregion



    }
}
