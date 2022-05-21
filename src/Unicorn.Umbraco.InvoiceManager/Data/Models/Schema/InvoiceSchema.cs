using Examine.Search;
using Unicorn.Umbraco.InvoiceManager.Enums;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Unicorn.Umbraco.InvoiceManager.Models.Schema
{
    [TableName(TableName)]
    [PrimaryKey(nameof(Id), AutoIncrement = true)]
    [ExplicitColumns]
    public class InvoiceSchema
    {
        #region Constants

        /// <summary>
        /// Gets the name of the table used in the database.
        /// </summary>
        public const string TableName = "UnicornInvoiceManagerInvoices";

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Column(nameof(Id))]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column(nameof(CustomerId))]
        [ForeignKey(typeof(CustomerSchema), Name = "FK_Invoice_CustomerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(nameof(Status))]
        public int Status { get; set; }

        [Column(nameof(PaymentStatus))]
        public int PaymentStatus { get; set; }


        [Column(nameof(InvoiceDate))]
        public DateTime InvoiceDate { get; set; }

        [Column(nameof(DueDate))]
        public DateTime DueDate { get; set; }

        [Column(nameof(InvoiceNote))]
        public string InvoiceNote { get; set; }

        [Column(nameof(DateCreated))]
        public DateTime DateCreated { get; set; }

        [Column(nameof(DateModified))]
        public DateTime DateModified { get; set; }

        [Column(nameof(IsDeleted))]
        public bool IsDeleted { get; set; }

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
