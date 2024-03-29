﻿using Mahzan.Models.Enums.CompaniesAdresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.DTO.Company.UpdateCompany
{
    public class UpdateCompanyDto
    {
        public CompanyDto CompanyDto { get; set; }

        public List<CompanyAdressDto> CompanyAdressesDto { get; set; }
    }

    public class CompanyDto
    {
        public string RFC { get; set; }

        public string CURP { get; set; }

        public string CommercialName { get; set; }

        public string BusinesslName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public Guid MemberId { get; set; }

        public Guid TaxRegimeCodeId { get; set; }

        public string OfficePhone { get; set; }

        public string MobilePhone { get; set; }

        public string AdditionalInformation { get; set; }
    }

    public class CompanyAdressDto
    {
        public AddressTypeEnum AdressType { get; set; }

        public string Street { get; set; }

        public string ExteriorNumber { get; set; }

        public string InternalNumber { get; set; }

        public string PostalCode { get; set; }

        public Guid CompanyId { get; set; }
    }
}
