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
    public class Invoice : IInvoice
    {
        public Invoice()
        {
            Dto = new InvoiceDto();
        }
        internal InvoiceDto Dto { get; }
        public int InvoiceId { get => Dto.InvoiceId; set => Dto.InvoiceId=value; }
        public int CustomerId { get => Dto.CustomerId; set => Dto.CustomerId=value; }
        public int Status { get => Dto.Status; set => Dto.Status=value; }
        public DateTime InvoiceDate { get => Dto.InvoiceDate.ToLocalTime(); set => Dto.InvoiceDate=value.ToUniversalTime(); }
        public DateTime DueDate { get => Dto.DueDate.ToLocalTime(); set => Dto.DueDate=value.ToUniversalTime(); }
        public string InvoiceNote { get => Dto.InvoiceNote; set => Dto.InvoiceNote=value; }
        public string Description { get => Dto.Description; set => Dto.Description = value; }
        public int Quantity { get => Dto.Quantity; set => Dto.Quantity=value; }
        public decimal UnitPrice { get => Dto.UnitPrice; set => Dto.UnitPrice=value; }
        public decimal GST { get => Dto.GST; set => Dto.GST=value; }
        public DateTime DateCreated { get => Dto.DateCreated.ToLocalTime(); set => Dto.DateCreated=value.ToUniversalTime(); }
        public DateTime DateModified { get => Dto.DateModified.ToLocalTime(); set => Dto.DateModified=value.ToUniversalTime(); }
        public bool IsDeleted { get => Dto.IsDeleted; set => Dto.IsDeleted=value; }
        public decimal TotalAmount { get => (this.TotalTax+(Quantity*Dto.UnitPrice)); }
        public decimal TotalTax { get => ((Dto.GST * Dto.Quantity * Dto.UnitPrice)/100); }
        public ICustomer Customer { get => new Customer(Dto.Customer); }
        public string InvoiceNumber { get => Dto.InvoiceId.ToString("D6"); }

        public IInvoiceData InvoiceData{ get=>new InvoiceData(Dto); }

        internal Invoice(InvoiceDto dto)
        {
            Dto = dto;
        }
        #region Static methods

        internal static Invoice CreateFromDto(InvoiceDto dto)
        {
            return dto == null ? null : new Invoice(dto);
        }

        #endregion
    }


}
