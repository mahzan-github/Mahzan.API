using Mahzan.Business.V1.Events.Company;
using Mahzan.Business.V1.Validations.Company.SaveCompany;
using Mahzan.Dapper.V1.DTO.Company.CreateCompany;
using Mahzan.Dapper.V1.DTO.Company.UpdateCompany;
using Mahzan.Dapper.V1.Filters._Base.Companies;
using Mahzan.Dapper.V1.Repositories._Base.Companies;
using Mahzan.Dapper.V1.Repositories._Base.EventsLog;
using Mahzan.Dapper.V1.Repositories.Company.CreateCompany;
using Mahzan.Dapper.V1.Repositories.Company.UpdateCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Business.V1.EventsHandlers.Company.SaveCompany
{
    public class SaveCompanyEventHandler : ISaveCompanyEventHandler
    {
        private readonly ICompaniesRepository _companiesRepository;

        private readonly ICreateCompanyRepository _createCompanyRepository;

        private readonly IUpdateCompanyRepository _updateCompanyRepository;

        private readonly ISaveCompanyValidations _saveCompanyValidations;

        public SaveCompanyEventHandler(
             ICompaniesRepository companiesRepository,
             ICreateCompanyRepository createCompanyRepository,
             IUpdateCompanyRepository updateCompanyRepository, 
             ISaveCompanyValidations saveCompanyValidations)
        {
            _companiesRepository = companiesRepository;
            _createCompanyRepository = createCompanyRepository;
            _updateCompanyRepository = updateCompanyRepository;
            _saveCompanyValidations = saveCompanyValidations;
        }

        public async Task<Guid> Handler(SaveCompanyEvent saveCompanyEvent)
        {
            Guid companyId = Guid.Empty;

            //Valida Datos de Evento
            await _saveCompanyValidations
                .Handle(saveCompanyEvent);


            List<Models.Entities.Companies> companies;
            companies = await _companiesRepository
                .GetCompaniesByFilterAsync(
                new GetCompaniesFilter { 
                    MemberId = saveCompanyEvent.MemberId,
                    CompanyId = saveCompanyEvent.CompanyEvent.CompanyId
                });

            if (!companies.Any())
            {
                companyId = await CreateCompany(saveCompanyEvent);
            }
            else 
            {
                companyId = await UpdateCompany(saveCompanyEvent);
            }



            return companyId;
        }

        private async Task<Guid> CreateCompany(SaveCompanyEvent createCompanyEvent) 
        {
            Guid companyId = Guid.Empty;

            companyId = await _createCompanyRepository
                .Handle(new CreateCompanyDto
                {
                    CompanyDto = new Dapper.V1.DTO.Company.CreateCompany.CompanyDto
                    {
                        RFC = createCompanyEvent.CompanyEvent.RFC,
                        CURP = createCompanyEvent.CompanyEvent.CURP,
                        CommercialName = createCompanyEvent.CompanyEvent.CommercialName,
                        BusinessName = createCompanyEvent.CompanyEvent.BusinessName,
                        Email = createCompanyEvent.CompanyEvent.Email,
                        TaxRegimeCodeId = createCompanyEvent.CompanyEvent.TaxRegimeCodeId,
                        OfficePhone = createCompanyEvent.CompanyEvent.OfficePhone,
                        MobilePhone = createCompanyEvent.CompanyEvent.MobilePhone,
                        AdditionalInformation = createCompanyEvent.CompanyEvent.AdditionalInformation
                    },
                    CompanyAdressesDto = createCompanyEvent
                                               .CompanyAdressesEvent
                                               .Select(c => new Dapper.V1.DTO.Company.CreateCompany.CompanyAdressDto
                                               {
                                                   AdressType = c.AdressType,
                                                   Street = c.Street,
                                                   ExteriorNumber = c.ExteriorNumber,
                                                   InternalNumber = c.InternalNumber,
                                                   PostalCode = c.PostalCode,
                                                   CompanyId = c.CompanyId
                                               })
                                               .ToList(),
                    MemberId = createCompanyEvent.MemberId
                });

            return companyId;
        }

        private async Task<Guid> UpdateCompany(SaveCompanyEvent createCompanyEvent) 
        {
            Guid companyId = Guid.Empty;

            companyId = await _updateCompanyRepository
                .Handle(
                new UpdateCompanyDto { 

                });

            return companyId;
        }
    }
}
