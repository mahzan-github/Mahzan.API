using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Models.Entities
{
    public class CompaniesAdresses
    {
        public Guid CompanyAdressId { get; set; }

        public string AdressType { get; set; }

        public string Street { get; set; }

        public string ExteriorNumber { get; set; }

        public string InteriorNumber { get; set; }

        public string PostalCode { get; set; }

        public Guid CompanyId { get; set; }
    }
}
