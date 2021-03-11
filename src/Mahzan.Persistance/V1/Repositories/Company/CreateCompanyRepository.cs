using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.Company;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Company
{
    public class CreateCompanyRepository:BaseRepository<CreateCompanyDto>,
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
                        rfc= dto.CompanyDto.RFC,
                        curp= dto.CompanyDto.CURP,
                        comercial_name= dto.CompanyDto.CommercialName,
                        business_name= dto.CompanyDto.BusinessName,
                        email = dto.CompanyDto.Email,
                        active = true,
                        member_id = dto.CompanyDto.MemberId,
                        tax_regime_code_id = dto.CompanyDto.TaxRegimeCodeId,
                        office_phone = dto.CompanyDto.OfficePhone,
                        mobile_phone = dto.CompanyDto.MobilePhone,
                        additional_information = dto.CompanyDto.AdditionalInformation
                    }
                );

            return dto with
            {
               CompanyDto = dto.CompanyDto with
               {
                   CompanyId = companyId
               }
            };
        }
    }
}