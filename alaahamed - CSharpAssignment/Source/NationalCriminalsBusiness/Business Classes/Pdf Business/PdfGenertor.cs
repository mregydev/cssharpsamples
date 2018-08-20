using NationalCriminalsBusiness.Entities;
using NationalCriminalsBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness
{
    //Context class
    public class PdfGenertor
    {
        #region Fields
        private static IPdfGenerator _pdfGenerator;

        #endregion

        #region Constructor
        private PdfGenertor()
        {

        }

        #endregion

        #region Methods
        /// <summary>
        /// generate pdf file for profile information of the passed criminal
        /// </summary>
        /// <param name="criminal"></param>
        public static bool GeneratorPdf(CriminalProxy criminal)
        {
            if (_pdfGenerator == null)
            {
                _pdfGenerator = new TextSharpGenerator();
            }

            return _pdfGenerator.GeneratePdf(criminal);

        }

        #endregion
    }
}
