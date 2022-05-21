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
    public class Invoice : IInvoice
    {
        public Invoice()
        {
            Dto = new InvoiceDto();
        }
        internal InvoiceDto Dto { get; }
        internal InvoiceItemsDto ItemsDto { get; }
        public int InvoiceId { get => Dto.Id; set => Dto.Id = value; }
        public int CustomerId { get => Dto.CustomerId; set => Dto.CustomerId = value; }

        public int PaymentStatus { get => Dto.PaymentStatus; set => Dto.PaymentStatus= value; }

        public string InvoiceNote { get => Dto.InvoiceNote; set => Dto.InvoiceNote = value; }

        public int Status { get => Dto.Status; set => Dto.Status = value; }
        public DateTime InvoiceDate { get => Dto.InvoiceDate.ToLocalTime(); set => Dto.InvoiceDate = value.ToUniversalTime(); }
        public DateTime DueDate { get => Dto.DueDate.ToLocalTime(); set => Dto.DueDate = value.ToUniversalTime(); }
        public DateTime DateCreated { get => Dto.DateCreated.ToLocalTime(); set => Dto.DateCreated = value.ToUniversalTime(); }
        public DateTime DateModified { get => Dto.DateModified.ToLocalTime(); set => Dto.DateModified = value.ToUniversalTime(); }
        public bool IsDeleted { get => Dto.IsDeleted; set => Dto.IsDeleted = value; }
        public ICustomer Customer { get => new Customer(Dto.Customer); }
        public string InvoiceNumber { get => Dto.Id.ToString("D8"); }

        public List<IInvoiceData> InvoiceData { get => Dto.InvoiceItems.Select(x => new InvoiceData(x)).ToList<IInvoiceData>();
            set => Dto.InvoiceItems = value.Select(x =>
            new InvoiceItemsDto() {
                Id = x.Id,
                DateCreated = x.DateCreated,
                DateModified = x.DateModified,
                Description = x.Description,
                GST = x.GST,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                InvoiceId = x.InvoiceId

            }).ToList();                                     
        }

        public decimal TotalAmount { get=> this.InvoiceData.Sum(x=>x.TotalAmount);}

        public decimal TaxableAmount { get=>this.InvoiceData.Sum(x=>x.TaxableAmount);}

        public decimal TotalTax { get => InvoiceData.Sum(x => x.TotalTax); }

        internal Invoice(InvoiceDto dto)
        {
            Dto = dto;
        }
        internal Invoice(InvoiceItemsDto dto)
        {
            ItemsDto = dto;
        }
        #region Static methods

        internal static Invoice CreateFromDto(InvoiceDto dto)
        {
            return dto == null ? null : new Invoice(dto);
        }

        #endregion
    }


}
