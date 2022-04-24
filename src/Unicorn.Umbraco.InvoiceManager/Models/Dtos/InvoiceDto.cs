using Examine.Search;
using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Unicorn.Umbraco.InvoiceManager.Models.Dtos
{
    [TableName(TableName)]
    [PrimaryKey(nameof(InvoiceId), AutoIncrement = true)]
    [ExplicitColumns]
    public class InvoiceDto
    {
        #region Constants

        /// <summary>
        /// Gets the name of the table used in the database.
        /// </summary>
        public const string TableName = "Invoices";

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Column(nameof(InvoiceId))]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int InvoiceId { get; set; }

        [Column(nameof(CustomerId))]
        [ForeignKey(typeof(CustomerDto), Name = "FK_Invoice_CustomerId")]
        public int CustomerId { get; set; }


        [Column(nameof(Status))]
        public int Status { get; set; }

        [Column(nameof(InvoiceDate))]
        public DateTime InvoiceDate { get; set; }

        [Column(nameof(DueDate))]
        public DateTime DueDate { get; set; }

        [Column(nameof(InvoiceNote))]
        public string InvoiceNote { get; set; }

        [Column(nameof(Description))]
        public string Description { get; set; }

        [Column(nameof(Quantity))]
        public int Quantity { get; set; }

        [Column(nameof(UnitPrice))]
        public decimal UnitPrice { get; set; }

        [Column(nameof(GST))]
        public decimal GST { get; set; }

        [Column(nameof(DateCreated))]
        public DateTime DateCreated { get; set; }

        [Column(nameof(DateModified))]
        public DateTime DateModified { get; set; }


        [Column(nameof(IsDeleted))]
        public bool IsDeleted { get; set; }

        [ResultColumn]
        [References(typeof(CustomerDto))]
        [Reference(ReferenceType.OneToOne, ReferenceMemberName = "CustomerId",ColumnName ="CustomerId")]
        public CustomerDto Customer { get; set; }

        /// <summary>
        /// This field is ignored by db.CreateTable().
        /// </summary>
        [Ignore]
        public InvoiceStatus InvoiceStatus
        {
            get
            {
                return (InvoiceStatus)Status;
            }
            set
            {
                Status = (int)value;
            }
        }
        #endregion
    }
}
