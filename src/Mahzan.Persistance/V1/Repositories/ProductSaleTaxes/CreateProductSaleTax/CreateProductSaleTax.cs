using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductSaleTaxes;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductSaleTaxes.CreateProductSaleTax
{
    public class CreateProductSaleTax : BaseInsertRepository<CreateProductSaleTaxDto>,
        ICreateProductSaleTax
    {
        public CreateProductSaleTax(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<CreateProductSaleTaxDto> InsertInternal(CreateProductSaleTaxDto dto)
        {
            foreach (var productTaxDto in dto.ListProductTaxDto)
            {
                   
                string sql = @"
                insert into product_sale_taxes
                (
                 product_sale_tax_id,
                 product_tax_id,
                 product_id
                )
                values
                (
                 @product_sale_tax_id,
                 @product_tax_id,
                 @product_id      
                )
                returning product_sale_tax_id;
                ";

                await Connection
                    .ExecuteScalarAsync<Guid>(
                        sql,
                        new
                        {
                            product_sale_tax_id = Guid.NewGuid(),
                            product_tax_id   = productTaxDto.ProductTaxId,
                            product_id = productTaxDto.ProductId
                        }
                    );
            }

            return dto;
        }

    }
}