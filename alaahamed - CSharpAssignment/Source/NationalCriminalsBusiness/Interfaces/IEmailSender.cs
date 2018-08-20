using NationalCriminalsBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness.Interfaces
{
    interface IEmailSender
    {
        void SendCriminalProfile(string reciever, List<CriminalProxy> criminals);
        void SendEmail(string receiver, string content, string subject = "");
    }
}
