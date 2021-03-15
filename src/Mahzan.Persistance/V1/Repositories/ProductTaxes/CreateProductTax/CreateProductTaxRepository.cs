using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductTaxes;
using Mahzan.Persistance.V1.Exeptions.ProductTaxes.CreateProductTax;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductTaxes.CreateProductTax
{
    public class CreateProductTaxRepository
        :BaseInsertRepository<CreateProductTaxDto>,
            ICreateProductTaxRepository
    {

        
        public CreateProductTaxRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {

        }

        protected override async Task<CreateProductTaxDto> InsertInternal(
            CreateProductTaxDto dto)
        {
            Guid productTaxId = await InsertInProductTaxes(dto);

            return dto with
            {
                ProductTaxId = productTaxId
            };
        }

        protected override void HandlePrevalidations(CreateProductTaxDto dto)
        {
            if (!CompanyExist(dto.CompanyId))
            {
                throw new CreateProductTaxArgumentException(
                    $"El CompanyId {dto.CompanyId} no existe.");
            }
            
            if (NameExist(dto.Name))
            {
                throw new CreateProductTaxArgumentException(
                    $"El nombre {dto.Name} del impuesto ya existe.");
            } 
        }
        
        #region :: Insert Product Tax Steps::

        private async Task<Guid> InsertInProductTaxes(
            CreateProductTaxDto dto)
        {
            Guid productTaxId = Guid.Empty;;
            
            string sql = @"
                insert into product_taxes
                (
                 product_tax_id,
                 name,
                 percentage,
                 print_on_ticket,
                 company_id
                )
                values
                (
                 @product_tax_id,
                 @name,
                 @percentage,
                 @print_on_ticket,
                 @company_id
                )
                returning product_tax_id;
            ";

            productTaxId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_tax_id = Guid.NewGuid(),
                        name = dto.Name,
                        percentage = dto.Percentage,
                        print_on_ticket = dto.PrintOnTicket,
                        company_id = dto.CompanyId
                    }
                );
                
            return productTaxId;
        }

        #endregion
        
        #region :: Prevalidations ::

        private bool NameExist(string name)
        {
            bool result = false;
            
            string sql = @"
                select * from product_taxes
                where name = @name
            ";

            IEnumerable<Models.Entities.ProductTaxes> productTaxes;
            productTaxes = Connection
                .Query<Models.Entities.ProductTaxes>(
                    sql,
                    new
                    {
                        name= name
                    });

            if (productTaxes.Any())
            {
                result = true;
            }

            return result;
        }

        private bool CompanyExist(Guid companyId)
        { 
            bool result = false;
            
            string sql = @"
                select * from companies
                where company_id = @company_id
            ";

            IEnumerable<Models.Entities.Companies> companies;
            companies = Connection
                .Query<Models.Entities.Companies>(
                    sql,
                    new
                    {
                        company_id= companyId
                    });

            if (companies.Any())
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}