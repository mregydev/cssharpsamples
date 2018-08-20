using NationalCriminalsDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness.Entities
{
    [DataContract]
    public class UserProxy
    {

        #region Fields
        private User _userInstance;

        #endregion

        #region Constructor
        public UserProxy()
        {
            this._userInstance = new User();
        }

        #endregion

        #region Properties
        [DataMember]
        public string UserName
        {
            set
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    this._userInstance.UserName = value;
                }
            }

            get
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }

                return this._userInstance.UserName;
            }

        }



        [DataMember]
        public string Email
        {
            set
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                    if (regex.IsMatch(value))
                    {
                        this._userInstance.Email = value;
                    }
                }
            }

            get
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }

                return this._userInstance.Email;
            }
        }


        [DataMember]
        public string Password
        {
            set
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }


                if (!string.IsNullOrEmpty(value))
                {
                    this._userInstance.Password = value;
                }
            }

            get
            {
                if (this._userInstance == null)
                {
                    this._userInstance = new User();
                }

                return this._userInstance.Password;
            }
        }


        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add Current instiated user instance which is data access entity to the data base 
        /// </summary>
        /// <returns></returns>
        public bool AddToDataBase()
        {
            if (this._userInstance != null && IsValid)
            {
                try
                {
                    this._userInstance.Id = Guid.NewGuid();

                    NationalCriminalModelDataContext dbContext = new NationalCriminalModelDataContext();
                    dbContext.Users.InsertOnSubmit(this._userInstance);
                    dbContext.SubmitChanges();

                    return true;
                }
                catch
                {

                }

            }

            return false;
        }



        /// <summary>
        /// retreive Current instiated user instance which is data access entity from the database 
        /// </summary>
        /// <returns></returns>
        public UserProxy GetInstance()
        {
            if (this._userInstance != null && IsValid)
            {
                try
                {
                    NationalCriminalModelDataContext dbContext = new NationalCriminalModelDataContext();

                    var currentUser = dbContext.Users.FirstOrDefault(user => user.UserName.ToLower() == this.UserName && user.Password == this.Password);

                    if (currentUser != null)
                    {
                        return new UserProxy { Email = currentUser.Email, UserName = currentUser.UserName, Password = currentUser.Password };
                    }
                    return null;
                }
                catch
                {

                }

            }

            return null;
        }

        #endregion
    }
}
