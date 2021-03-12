using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.Company;
using Mahzan.Persistance.V1.Exeptions.Company.CreateCompany;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Company
{
    public class CreateCompanyRepository:BaseInsertRepository<CreateCompanyDto>,
        ICreateCompanyRepository
    {
        public CreateCompanyRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {
            
        }

        protected override async Task<CreateCompanyDto> InsertInternal(
            CreateCompanyDto dto)
        {
            //Compañia
            Guid companyId = await InsertInCompanies(dto.CompanyDto);
            
            //Direcciones
            if (dto.CompanyAdressesDto.Any())
            {
                await InsetInCompaniesAddresses(
                    companyId,
                    dto.CompanyAdressesDto
                );
            }

            return dto with
            {
               CompanyDto = dto.CompanyDto with
               {
                   CompanyId = companyId
               }
            };
        }

        protected override async void HandlePrevalidations(CreateCompanyDto dto)
        {
            if (!TaxRegimeCodeIdExist(dto.CompanyDto.TaxRegimeCodeId))
            {
                throw new CreateCompanyArgumentException(
                    $"El TaxRegimeCodeId {dto.CompanyDto.TaxRegimeCodeId} no existe."
                );
            }
        }

        #region :: Insert Company Steps ::
        private async Task<Guid> InsertInCompanies(CompanyDto companyDto)
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
                        business_name= companyDto.BusinessName,
                        email = companyDto.Email,
                        active = true,
                        member_id =companyDto.MemberId,
                        tax_regime_code_id = companyDto.TaxRegimeCodeId,
                        office_phone = companyDto.OfficePhone,
                        mobile_phone = companyDto.MobilePhone,
                        additional_information = companyDto.AdditionalInformation
                    }
                );

            return companyId;
        }

        private async Task InsetInCompaniesAddresses(
            Guid companyId,
            List<CompanyAdressDto> companyAdressesDto)
        {
            foreach (var companyAdresseDto in companyAdressesDto)
            {
                string sql = @"
                    insert into companies_addresses
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
                        @company_id
                    )
                ";

                await Connection
                    .ExecuteAsync(
                        sql,
                        new
                        {
                            company_adress_id = Guid.NewGuid(),
                            adress_type = companyAdresseDto.AdressType.ToString(),
                            street = companyAdresseDto.Street,
                            exterior_number = companyAdresseDto.ExteriorNumber,
                            internal_number = companyAdresseDto.InternalNumber,
                            postal_code = companyAdresseDto.PostalCode,
                            company_id = companyId
                        });

            }
        }
        
        #endregion

        #region :: Prevalidations ::
        private bool TaxRegimeCodeIdExist(Guid taxRegimeCodeId)
        {
            bool result = false;
            
            string sql = @"
                select * from tax_regime_codes
                where tax_regime_code_id = @tax_regime_code_id
            ";

            IEnumerable<Models.Entities.TaxRegimeCodes> taxRegimeCodes;
            taxRegimeCodes = Connection
                .Query<Models.Entities.TaxRegimeCodes>
                (
                    sql,
                    new
                    {
                        tax_regime_code_id = taxRegimeCodeId
                    }
                );

            if (taxRegimeCodes.Any())
            {
                result = true;
            }

            return result;
        }
        
        #endregion
    }
}