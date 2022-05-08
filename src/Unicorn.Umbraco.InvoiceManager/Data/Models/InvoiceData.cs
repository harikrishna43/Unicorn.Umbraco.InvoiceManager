using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models
{
    public class InvoiceData : IInvoiceData
    {
        public InvoiceData()
        {
            Dto = new InvoiceItemsDto();
        }
        internal InvoiceItemsDto Dto { get; }
        
        public int Quantity { get => Dto.Quantity; set => Dto.Quantity=value; }
        public decimal UnitPrice { get => Dto.UnitPrice; set => Dto.UnitPrice=value; }
        public decimal GST { get => Dto.GST; set => Dto.GST=value; }
        public decimal TaxableAmount { get => (Quantity * Dto.UnitPrice); }
        public decimal TotalTax { get => ((Dto.GST * Dto.Quantity * Dto.UnitPrice) / 100); }
        public decimal TotalAmount { get => (this.TotalTax+(Quantity*Dto.UnitPrice)); }
        public int Id { get => Dto.Id; set => Dto.Id=value; }
        public string InvoiceNote { get => Dto.InvoiceNote; set => Dto.InvoiceNote = value; }
        public string Description { get => Dto.Description; set => Dto.Description = value; }
        public DateTime DateCreated { get => Dto.DateCreated; set => Dto.DateCreated = value; }
        public DateTime DateModified { get => Dto.DateModified; set => Dto.DateModified = value; }
        public int InvoiceId { get => Dto.InvoiceId; set => Dto.InvoiceId=value; }

        internal InvoiceData(InvoiceItemsDto dto)
        {
            Dto = dto;
        }
        #region Static methods

        internal static InvoiceData CreateFromDto(InvoiceItemsDto dto)
        {
            return dto == null ? null : new InvoiceData(dto);
        }

        #endregion
    }
}
