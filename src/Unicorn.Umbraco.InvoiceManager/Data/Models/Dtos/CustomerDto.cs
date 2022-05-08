using Unicorn.Umbraco.InvoiceManager.Enums;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Unicorn.Umbraco.InvoiceManager.Models.Dtos
{
    [TableName(TableName)]
    [PrimaryKey(nameof(CustomerId), AutoIncrement = true)]
    [ExplicitColumns]
    public class CustomerDto
    {
        #region Constants
        /// <summary>
        /// Gets the name of the table used in the database.
        /// </summary>
        public const string TableName = "UnicornInvoiceManagerCustomers";
        #endregion

        #region Properties

        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column(nameof(CustomerId))]
        public int CustomerId { get; set; }

        [Column(nameof(Name))]
        public string Name { get; set; }

        [Column(nameof(CustomerType))]
        public int CustomerType { get; set; }

        [Column(nameof(GSTNumber))]
        public string GSTNumber { get; set; }

        [Column(nameof(Phone))]
        public string Phone { get; set; }

        [Column(nameof(Address))]
        public string Address { get; set; }

        [Column(nameof(City))]
        public string City { get; set; }

        [Column(nameof(State))]
        public string State { get; set; }

        [Column(nameof(Country))]
        public string Country { get; set; }

        [Column(nameof(ZipCode))]
        public string ZipCode { get; set; }

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
        public CustomerType Type
        {
            get
            {
                return (CustomerType)CustomerType;
            }
            set
            {
                CustomerType = (int)value;
            }
        }
        #endregion
    }
}
