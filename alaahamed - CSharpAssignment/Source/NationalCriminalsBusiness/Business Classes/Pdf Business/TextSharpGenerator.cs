using iTextSharp.text;
using iTextSharp.text.pdf;
using NationalCriminalsBusiness.Entities;
using NationalCriminalsBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness
{
    class TextSharpGenerator : IPdfGenerator
    {
        public bool GeneratePdf(CriminalProxy criminal)
        {

            try
            {
                if (criminal != null && criminal.IsValid)
                {

                    //create new file or override existing one 
                    FileStream fs = new FileStream(criminal.FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

                    //create new pdf document
                    Document doc = new Document();


                    //create new pdf writer instance
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);


                    doc.Open();


                    //fill pdf with information of each criminal 
                    //reflection is used to get value of each property and add it to pdf content
                    foreach (var propertyInfo in criminal.GetType().GetProperties())
                    {
                        if (propertyInfo.Name.ToLower() != "filepath" && propertyInfo.Name.ToLower() != "isvalid")
                        {
                            var content = propertyInfo.Name + " : " + propertyInfo.GetValue(criminal);

                            doc.Add(new Paragraph(content));
                        }

                    }

                    //close docuemnt
                    doc.Close();

                    return true;
                }

                //If criminal or path arenot valid return false
                return false;

            }
            catch
            {
                return false;
            }
        }
    }
}
