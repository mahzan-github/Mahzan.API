using System;
using Mahzan.Persistance.V1.Dto.Company;

namespace Mahzan.Persistance.V1.ViewModel.Company
{
    public class CreateCompanyViewModel
    {
        public Guid CompanyId { get; set; }

        public static CreateCompanyViewModel From(CreateCompanyDto createCompanyDto)
        {
            return new ()
            {
                CompanyId = createCompanyDto.CompanyDto.CompanyId
            };
        }
    }
}