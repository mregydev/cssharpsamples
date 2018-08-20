using NationalCriminalsBusiness;
using NationalCriminalsBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NationalCriminalsWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface INationalCriminalSvc
    {

        [OperationContract]
        bool AddUser(UserProxy user);


        [OperationContract]
        bool AddCriminal(CriminalProxy criminal);


        [OperationContract]
        bool SearchCrininals(string email, CriminalSearchCriteria searchCriteria);

        /// <summary>
        /// Get user instance from the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        UserProxy GetUserInstanceFromDb(UserProxy user);
    }

}
