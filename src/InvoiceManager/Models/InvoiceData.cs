using Invoice_Manager.Enums;
using Invoice_Manager.Interfaces;
using Invoice_Manager.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Models
{
    public class InvoiceData : IInvoiceData
    {
        public InvoiceData()
        {
            Dto = new InvoiceDto();
        }
        internal InvoiceDto Dto { get; }
        
        public int Quantity { get => Dto.Quantity; set => Dto.Quantity=value; }
        public decimal UnitPrice { get => Dto.UnitPrice; set => Dto.UnitPrice=value; }
        public decimal GST { get => Dto.GST; set => Dto.GST=value; }
        public decimal TaxableAmount { get => (Quantity * Dto.UnitPrice); }
        public decimal TotalTax { get => ((Dto.GST * Dto.Quantity * Dto.UnitPrice) / 100); }
        public decimal TotalAmount { get => (this.TotalTax+(Quantity*Dto.UnitPrice)); }

        internal InvoiceData(InvoiceDto dto)
        {
            Dto = dto;
        }
        #region Static methods

        internal static InvoiceData CreateFromDto(InvoiceDto dto)
        {
            return dto == null ? null : new InvoiceData(dto);
        }

        #endregion
    }
}
