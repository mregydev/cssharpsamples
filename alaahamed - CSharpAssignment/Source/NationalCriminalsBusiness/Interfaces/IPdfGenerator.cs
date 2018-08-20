using NationalCriminalsBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalCriminalsBusiness.Interfaces
{
    interface IPdfGenerator
    {
        bool GeneratePdf(CriminalProxy criminal);
    }
}
