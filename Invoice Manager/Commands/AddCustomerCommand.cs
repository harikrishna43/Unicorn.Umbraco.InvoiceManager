﻿using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Commands
{
    public class AddCustomerCommand : ICommand
    {
        public string Name { get; set; }

        public int CustomerType { get; set; }

        public string GSTNumber {get; set;}

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}