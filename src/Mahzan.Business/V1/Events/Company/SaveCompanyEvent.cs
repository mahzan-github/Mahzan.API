using Mahzan.Business.Events._Base;
using Mahzan.Models.Enums.CompaniesAdresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Business.V1.Events.Company
{
    public class SaveCompanyEvent: EventBase
    {
        public CompanyEvent CompanyEvent { get; set; }

        public List<CompanyAdressEvent> CompanyAdressesEvent { get; set; }
    }

    public class CompanyEvent
    {
        public Guid CompanyId { get; set; }

        public string RFC { get; set; }

        public string CURP { get; set; }

        public string CommercialName { get; set; }

        public string BusinessName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public Guid TaxRegimeCodeId { get; set; }

        public string OfficePhone { get; set; }

        public string MobilePhone { get; set; }

        public string AdditionalInformation { get; set; }
    }

    public class CompanyAdressEvent
    {
        public AddressTypeEnum AdressType { get; set; }

        public string Street { get; set; }

        public string ExteriorNumber { get; set; }

        public string InternalNumber { get; set; }

        public string PostalCode { get; set; }

        public Guid CompanyId { get; set; }
    }
}
