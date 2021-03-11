using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahzan.Models.Enums.CompaniesAdresses;

namespace Mahzan.Business.V1.Commands.Company
{
    public class CreateCompanyCommand
    {
        public CompanyCommand CompanyCommand { get; set; }

        public List<CompanyAdressCommand> CompanyAdressesCommand { get; set; }
    }

    public class CompanyCommand
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
        
        public Guid MemberId { get; set; }
    }

    public class CompanyAdressCommand
    {
        [Required]
        public AddressTypeEnum AdressType { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string ExteriorNumber { get; set; }

        public string InternalNumber { get; set; }

        [Required]
        [MaxLength(4)]
        public string PostalCode { get; set; }
        
    }
}