using Unicorn.Umbraco.InvoiceManager.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Sections;


namespace Unicorn.Umbraco.InvoiceManager.Section
{
    public class InvoiceManagerSection : ISection
    {
        public string Alias => ConstantVariables.SectionAlias;

        public string Name => ConstantVariables.SectionName;
    }
}
