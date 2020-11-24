using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Models.Entities
{
    public class Companies
    {
        public Guid CompanyId { get; set; }

        public string RFC { get; set; }

        public string CURP { get; set; }

        public string CommercialInfo { get; set; }

        public string BusinesslInfo { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public Guid MemberId { get; set; }

        public Guid TaxRegimeCodeId { get; set; }

        public string OfficePhone { get; set; }

        public string MobilePhone { get; set; }

        public string AdditionalInformation { get; set; }
    }
}
