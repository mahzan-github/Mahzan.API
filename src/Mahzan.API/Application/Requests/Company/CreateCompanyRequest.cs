using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahzan.Models.Enums.CompaniesAdresses;

namespace Mahzan.API.Application.Requests.Company
{
    public class CreateCompanyRequest
    {
        public CompanyRequest CompanyRequest { get; set; }

        public List<CompanyAdressRequest> CompanyAdressesRequest { get; set; }
    }

    public class CompanyRequest
    {
        [Required]
        [MaxLength(13)]
        public string RFC { get; set; }

        [MaxLength(18)]
        public string CURP { get; set; }

        [Required]
        public string CommercialName { get; set; }

        public string BusinessName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Guid TaxRegimeCodeId { get; set; }

        [Required]
        public string OfficePhone { get; set; }

        public string MobilePhone { get; set; }

        public string AdditionalInformation { get; set; }
    }

    public class CompanyAdressRequest
    {
        public AddressTypeEnum AdressType { get; set; }

        public string Street { get; set; }

        public string ExteriorNumber { get; set; }

        public string InternalNumber { get; set; }

        public string PostalCode { get; set; }

        public Guid CompanyId { get; set; }
    }
}