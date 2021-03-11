using System;
using System.Collections.Generic;
using Mahzan.Models.Enums.CompaniesAdresses;

namespace Mahzan.Persistance.V1.Dto.Company
{
    public record CreateCompanyDto
    {
        public CompanyDto CompanyDto { get; init; }

        public List<CompanyAdressDto> CompanyAdressesDto { get; set; }
    }

    public record CompanyDto 
    {
        public Guid CompanyId { get; init; }
        public string RFC { get; init; }

        public string CURP { get; init; }

        public string CommercialName { get; init; }

        public string BusinessName { get; init; }

        public string Email { get; init; }

        public bool Active { get; init; }

        public Guid MemberId { get; init; }

        public Guid TaxRegimeCodeId { get; init; }

        public string OfficePhone { get; init; }

        public string MobilePhone { get; init; }

        public string AdditionalInformation { get; init; }
    }

    public class CompanyAdressDto
    {
        public AddressTypeEnum AdressType { get; init; }

        public string Street { get; init; }

        public string ExteriorNumber { get; init; }

        public string InternalNumber { get; init; }

        public string PostalCode { get; init; }

        public Guid CompanyId { get; init; }
    }
}