using Mahzan.Dapper.V1.DTO.Company.CreateCompany;
using Mahzan.Dapper.V1.Exceptions.Company.CreateCompany;
using Mahzan.Dapper.V1.Rules._Base.Companies;
using Mahzan.Dapper.V1.Rules.Filters.Companies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Dapper.V1.Repositories._Base.EventsLog;
using Mahzan.Dapper.V1.Repositories._Base.Companies;
using Mahzan.Dapper.V1.Filters._Base.Companies;
using Mahzan.Dapper.V1.DTO.EventsLog.CreateEventLog;
using Mahzan.Models.Enums.EventsLog;
using Newtonsoft.Json;

namespace Mahzan.Dapper.V1.Repositories.Company.CreateCompany
{
    public class CreateCompanyRepository : DataConnection,ICreateCompanyRepository
    {
        private readonly ICompaniesRules _companiesRules;

        private readonly ICompaniesRepository _companiesRepository;

        private readonly IEventsLogRepository _eventsLogRepository;

        public CreateCompanyRepository(
            IDbConnection dbConnection,
            ICompaniesRules companiesRules,
            ICompaniesRepository companiesRepository,
            IEventsLogRepository eventsLogRepository)
            : base(dbConnection)
        {
            _companiesRules = companiesRules;
            _companiesRepository = companiesRepository;
            _eventsLogRepository = eventsLogRepository;
        }

        public async Task<Guid> Handle(CreateCompanyDto createCompanyDto)
        {
            Guid companyId = Guid.Empty;
            //Ejecuta Reglas
            await ExecuteRules(createCompanyDto);

            //Transacción alta de compañia
            using (var trasaction = Connection.BeginTransaction())
            {
                companyId = await InsertIntoCompanies(createCompanyDto.CompanyDto);

                await InsertIntoCompaniesAddresses(
                    createCompanyDto.CompanyAdressesDto,
                    companyId);
            }

            //Inserta en Log de Eventos
            await InsertIntoEventsLog(
                companyId,
                createCompanyDto.UserId,
                createCompanyDto.UserName);

            return companyId;
        }

        private async Task ExecuteRules(CreateCompanyDto createCompanyDto) 
        {
            //Valida que el RFC no exista.
            if (await _companiesRules
                .CompanyExist(
                new CompanyExistFilter { 
                    RFC = createCompanyDto.CompanyDto.RFC
                }))
            {
                throw new CreateCompanyInvalidOperationException(
                    $@"La empresa con el RFC {createCompanyDto.CompanyDto.RFC} ya existe."
                    );
            }

            //Valida que el regimen fiscal exista
        }

        /// <summary>
        /// Inserta en Companies
        /// </summary>
        /// <param name="companyDto"></param>
        /// <returns></returns>
        private async Task<Guid> InsertIntoCompanies(
            CompanyDto companyDto)
        {
            Guid companyId = Guid.Empty;

            string sql = @"
                insert into companies
                (
                company_id,
                rfc,
                curp,
                comercial_name,
                business_name,
                email,
                active,
                member_id,
                tax_regime_code_id,
                office_phone,
                mobile_phone,
                additional_information
                )
                values
                (
                @company_id,
                @rfc,
                @curp,
                @comercial_name,
                @business_name,
                @email,
                @active,
                @member_id,
                @tax_regime_code_id,
                @office_phone,
                @mobile_phone,
                @additional_information
                )
                returning company_id;
            ";

            companyId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new {
                        company_id = Guid.NewGuid(),
                        rfc= companyDto.RFC,
                        curp= companyDto.CURP,
                        comercial_name= companyDto.CommercialName,
                        business_name= companyDto.BusinesslName,
                        email = companyDto.Email,
                        active = true,
                        member_id = companyDto.MemberId,
                        tax_regime_code_id = companyDto.TaxRegimeCodeId,
                        office_phone = companyDto.OfficePhone,
                        mobile_phone = companyDto.MobilePhone,
                        additional_information = companyDto.AdditionalInformation
                    }
                );

            return companyId;
        }

        /// <summary>
        /// Inserta las Direcciónes de la compañia (Ubicación Fiscal / Lugar de Expedición)
        /// </summary>
        /// <param name="companyAdressesDto"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private async Task InsertIntoCompaniesAddresses(
            List<CompanyAdressDto> companyAdressesDto,
            Guid companyId) 
        {

            foreach (var companyAdressDto in companyAdressesDto)
            {
                string sql = @"
                    insert into companies
                    (
                    company_adress_id,
                    adress_type,
                    street,
                    exterior_number,
                    internal_number,
                    postal_code,
                    company_id
                    )
                    values
                    (
                    @company_adress_id,
                    @adress_type,
                    @street,
                    @exterior_number,
                    @internal_number,
                    @postal_code,
                    @company_id,
                    );
                ";

                await Connection
                   .ExecuteScalarAsync<Guid>(
                       sql,
                       new
                       {
                           company_adress_id = Guid.NewGuid(),
                           adress_type = companyAdressDto.AdressType.ToString(),
                           street = companyAdressDto.Street,
                           exterior_number = companyAdressDto.ExteriorNumber,
                           internal_number = companyAdressDto.InternalNumber,
                           postal_code = companyAdressDto.PostalCode,
                           company_id = companyId
                       }
                   );
            }
        }

        private async Task InsertIntoEventsLog(
            Guid companyId,
            Guid userId,
            string userName) 
        {
            List<Models.Entities.Companies> companies;
            companies = await _companiesRepository
                .GetCompaniesByFilterAsync(
                    new GetCompaniesFilter
                    {
                        CompanyId = companyId
                    });


            await _eventsLogRepository
                .Create(new CreateEventLogDto
                {
                    Controller = ControllersEnum.COMPANY.ToString(),
                    Action = ActionsEnum.CREATE.ToString(),
                    OldValue = null,
                    NewValue = JsonConvert.SerializeObject(companies),
                    EventAt = DateTimeOffset.Now,
                    UserId = userId,
                    UserName = userName
                });
        }
    }
}
