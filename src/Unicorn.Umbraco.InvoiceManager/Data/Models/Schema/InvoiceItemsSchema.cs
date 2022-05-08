using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;

namespace Unicorn.Umbraco.InvoiceManager.Data.Models.Schema
{
    [TableName(TableName)]
    [PrimaryKey(nameof(Id), AutoIncrement = true)]
    [ExplicitColumns]
    public class InvoiceItemsSchema
    {
        #region Constants

        /// <summary>
        /// Gets the name of the table used in the database.
        /// </summary>
        public const string TableName = "UnicornInvoiceManagerInvoiceItems";

        #endregion

        #region Properties
        [Column(nameof(Id))]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column(nameof(InvoiceId))]
        [ForeignKey(typeof(InvoiceSchema), Name = "FK_Invoiceitems_invoiceId")]
        public int InvoiceId { get; set; }

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
        #endregion
    }
}
