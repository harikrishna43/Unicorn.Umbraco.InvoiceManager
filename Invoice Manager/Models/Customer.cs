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
    public class Customer : ICustomer
    {
        public Customer()
        {
            Dto = new CustomerDto();
        }
        internal CustomerDto Dto { get; }
        //public int CustomerId => Dto.CustomerId;

        public string Name { get => Dto.Name; set => Dto.Name = value; }
        public int CustomerType { get => Dto.CustomerType; set => Dto.Type=(CustomerType)value; }
        public string GSTNumber { get => Dto.GSTNumber; set => Dto.GSTNumber=value; }
        public string Address { get => Dto.Address; set => Dto.Address=value; }
        public string City { get => Dto.City; set => Dto.City=value; }
        public string State { get => Dto.State; set => Dto.State=value; }
        public string Country { get => Dto.Country; set => Dto.Country=value; }
        public string ZipCode { get => Dto.ZipCode; set => Dto.ZipCode=value; }
        public DateTime DateCreated { get => Dto.DateCreated.ToLocalTime(); set => Dto.DateCreated=value.ToUniversalTime(); }
        public DateTime DateModified { get => Dto.DateModified.ToLocalTime(); set => Dto.DateModified = value.ToUniversalTime(); }
        public int CustomerId { get => Dto.CustomerId; set => Dto.CustomerId = value; }
        public bool IsDeleted { get => Dto.IsDeleted; set => Dto.IsDeleted = value; }

        internal Customer(CustomerDto dto)
        {
            Dto = dto;
        }
        #region Static methods

        internal static Customer CreateFromDto(CustomerDto dto)
        {
            return dto == null ? null : new Customer(dto);
        }

        #endregion
    }
}
