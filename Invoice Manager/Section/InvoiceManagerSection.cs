using Invoice_Manager.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Sections;


namespace Invoice_Manager.Section
{
    public class InvoiceManagerSection : ISection
    {
        public string Alias => ConstantVariables.SectionAlias;

        public string Name => ConstantVariables.SectionName;
    }
}
