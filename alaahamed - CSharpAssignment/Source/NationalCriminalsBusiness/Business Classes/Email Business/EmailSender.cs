using NationalCriminalsBusiness.Entities;
using NationalCriminalsBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness.Business_Classes.Email_Business
{
    public class EmailSender
    {
        #region Fields
        /// <summary>
        /// Email Sender Interface
        /// </summary>
        private static IEmailSender _emailSender;

        #endregion

        #region Methods
        /// <summary>
        /// Send normal content mail
        /// </summary>
        /// <param name="reciver"></param>
        /// <param name="content"></param>
        /// <param name="subject"></param>
        public static void SendEmail(string reciver, string content, string subject = "")
        {
            if (_emailSender == null)
            {
                _emailSender = new SmtpEmailSender(); ;
            }

            _emailSender.SendEmail(reciver, content, subject);
        }



        /// <summary>
        /// Send criminals profile
        /// </summary>
        /// <param name="reciver"></param>
        /// <param name="criminals"></param>
        public static void SendEmail(string reciver, List<CriminalProxy> criminals)
        {
            if (_emailSender == null)
            {
                _emailSender = new SmtpEmailSender();
            }

            _emailSender.SendCriminalProfile(reciver, criminals);


        }
        #endregion
    }
}
