﻿using Mahzan.Business.V1.Events.Company;
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

        public SaveCompanyEventHandler(
             ICompaniesRepository companiesRepository,
             ICreateCompanyRepository createCompanyRepository,
             IUpdateCompanyRepository updateCompanyRepository)
        {
            _companiesRepository = companiesRepository;
            _createCompanyRepository = createCompanyRepository;
            _updateCompanyRepository = updateCompanyRepository;
        }

        public async Task<Guid> Handler(SaveCompanyEvent saveCompanyEvent)
        {
            Guid companyId = Guid.Empty;

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
