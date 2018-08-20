using NationalCriminalsBusiness.Enums;
using NationalCriminalsDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness.Entities
{
    [DataContract]
    public class CriminalProxy
    {

        #region Fields
        private Criminal _criminalInstance;

        #endregion

        #region Constructor
        public CriminalProxy()
        {
            _criminalInstance = new Criminal();
            _criminalInstance.Name = "";
            _criminalInstance.Nationality = "";
            _criminalInstance.Weight = 0;
            _criminalInstance.Height = 0;
            _criminalInstance.Age = 0;


        }


        private CriminalProxy(Criminal criminal)
        {
            _criminalInstance = criminal;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Check whether current created instance properties are valid 
        /// </summary>
        /// <returns></returns>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Nationality) && this.Height > 0 && this.Weight > 0 && this.Age > 0 && !string.IsNullOrEmpty(this.FilePath);
            }
        }


        /// <summary>
        /// Pdf file path that contains criminal pofile information
        /// </summary
        public string FilePath
        {
            set
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    this._criminalInstance.FilePath = value;
                }
            }

            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.FilePath;
            }
        }

        /// <summary>
        /// Criminal full name
        /// </summary>
        [DataMember]
        public string Name
        {
            set
            {
                if (value != null && !string.IsNullOrEmpty(value.Trim()))
                {
                    if (this._criminalInstance == null)
                    {
                        this._criminalInstance = new Criminal();
                    }

                    this._criminalInstance.Name = value;
                }
            }
            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.Name;
            }
        }


        [DataMember]
        /// <summary>
        /// Criminal age
        /// </summary>
        public double Age
        {
            set
            {
                if (value > 0)
                {
                    if (this._criminalInstance == null)
                    {
                        this._criminalInstance = new Criminal();
                    }

                    this._criminalInstance.Age = value;
                }
            }
            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.Age.Value;
            }
        }


        [DataMember]
        /// <summary>
        /// Criminal sex
        /// </summary>
        public GenderType Sex
        {
            set
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                this._criminalInstance.IsMale = value == GenderType.Male;
            }

            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.IsMale == true ? GenderType.Male : GenderType.Female;
            }
        }


        [DataMember]
        /// <summary>
        /// Criminal nationality
        /// </summary>
        public string Nationality
        {
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    if (this._criminalInstance == null)
                    {
                        this._criminalInstance = new Criminal();
                    }

                    this._criminalInstance.Nationality = value;
                }
            }
            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.Nationality;
            }
        }


        [DataMember]
        /// <summary>
        /// Criminal Weight
        /// </summary>
        public double Weight
        {
            set
            {
                if (value > 0)
                {
                    if (this._criminalInstance == null)
                    {
                        this._criminalInstance = new Criminal();
                    }

                    this._criminalInstance.Weight = value;
                }
            }
            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.Weight.Value;
            }
        }

        [DataMember]
        /// <summary>
        /// Criminal Height
        /// </summary>
        public double Height
        {
            set
            {
                if (value > 0)
                {
                    if (this._criminalInstance == null)
                    {
                        this._criminalInstance = new Criminal();
                    }

                    this._criminalInstance.Height = value;
                }
            }
            get
            {
                if (this._criminalInstance == null)
                {
                    this._criminalInstance = new Criminal();
                }

                return this._criminalInstance.Height.Value;
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds Criminal Entity to the database
        /// </summary>
        /// <returns></returns>
        public bool AddToDatabase()
        {
            if (this._criminalInstance != null && IsValid)
            {
                try
                {
                    if (PdfGenertor.GeneratorPdf(this))
                    {
                        var dbContext = new NationalCriminalModelDataContext();

                        this._criminalInstance.Id = Guid.NewGuid();

                        dbContext.Criminals.InsertOnSubmit(this._criminalInstance);

                        dbContext.SubmitChanges();

                        return true;
                    }
                }
                catch
                {

                }
            }

            return false;
        }


        /// <summary>
        /// Search for criminals
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public static List<CriminalProxy> GetCriminals(CriminalSearchCriteria searchCriteria)
        {

            var criminalsResult = new List<CriminalProxy>();

            var dbContext = new NationalCriminalModelDataContext();


            //Search based on search criteria
            var criminals = dbContext.Criminals.ToList();

            if (searchCriteria != null)
            {
                if (!string.IsNullOrEmpty(searchCriteria.Name))
                {
                    criminals = criminals.Where(criminal => criminal.Name.ToLower().Contains(searchCriteria.Name.ToLower())).ToList();
                }


                if (!string.IsNullOrEmpty(searchCriteria.Nationality))
                {
                    criminals = criminals.Where(criminal => criminal.Nationality.ToLower().Contains(searchCriteria.Nationality.ToLower())).ToList();
                }



                if (searchCriteria.MinWeight > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Weight > searchCriteria.MinWeight).ToList();
                }

                if (searchCriteria.MinHeight > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Height > searchCriteria.MinHeight).ToList();
                }

                if (searchCriteria.MinAge > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Age > searchCriteria.MinAge).ToList();
                }

                if (searchCriteria.MaxWeight > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Weight < searchCriteria.MaxWeight).ToList();
                }

                if (searchCriteria.MaxHeight > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Height < searchCriteria.MaxHeight).ToList();
                }

                if (searchCriteria.MaxAge > 0)
                {
                    criminals = criminals.Where(criminal => criminal.Age < searchCriteria.MaxAge).ToList();
                }



                if (!searchCriteria.IgnoreGender)
                {
                    criminals = criminals.Where(criminal => criminal.IsMale == searchCriteria.IsMale).ToList();
                }

            }



            foreach (var criminal in criminals)
            {
                if (criminalsResult.Count > searchCriteria.MaxResultCount)
                {
                    break;
                }

                criminalsResult.Add(new CriminalProxy(criminal));
            }

            return criminalsResult;

        }
        #endregion
    }
}
