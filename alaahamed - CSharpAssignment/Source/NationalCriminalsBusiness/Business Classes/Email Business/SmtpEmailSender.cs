using NationalCriminalsBusiness.Entities;
using NationalCriminalsBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness
{
    class SmtpEmailSender : IEmailSender
    {
        #region Methods
        /// <summary>
        /// Send criminal profile in mail 
        /// </summary>
        /// <param name="reciever"></param>
        /// <param name="criminals"></param>
        public void SendCriminalProfile(string reciever, List<CriminalProxy> criminals)
        {
            //Intialize new instance of th smtp client and its credentials 
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            //User credentials 
            string gmailUserName = "alaahamed1990@gmail.com";
            string gmailUserPassword = "Hafez#455";
            smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);


            //Create new mail message - set its properties - and then send it 
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(gmailUserName);
                message.Subject = "Criminals List";


                //Insitalize attachments which will be profile pdfs of the criminal
                foreach (var criminal in criminals)
                {
                    message.Attachments.Add(new Attachment(criminal.FilePath));
                }


                message.Body = "";
                message.IsBodyHtml = true;
                message.To.Add(reciever);
                try
                {
                    smtpClient.Send(message);
                }
                catch
                {
                }
            }
        }



        /// <summary>
        /// Send normial mail with text content
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="content"></param>
        /// <param name="subject"></param>
        public void SendEmail(string receiver, string content, string subject = "")
        {
            //Create new instance of smtp client and instiaze its properties
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            //Mail server credentials
            string gmailUserName = "alaahamed1990@gmail.com";
            string gmailUserPassword = "Hafez#455";
            smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);


            //Send mail message
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress(gmailUserName);
                message.Subject = subject;


                message.Body = content;
                message.IsBodyHtml = true;
                message.To.Add(receiver);
                try
                {
                    smtpClient.Send(message);
                }
                catch
                {
                }
            }
        }
        #endregion
    }
}
