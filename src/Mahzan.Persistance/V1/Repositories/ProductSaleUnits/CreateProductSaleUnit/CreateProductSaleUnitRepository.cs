using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductPurchaseUnits;
using Mahzan.Persistance.V1.Dto.ProductSaleUnits;
using Mahzan.Persistance.V1.Exeptions.ProductSaleUnits.CreateProductSaleUnit;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.Repositories.ProductPurchaseUnits.CreateProductPurchaseUnit;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductSaleUnits.CreateProductSaleUnit
{
    public class CreateProductSaleUnitRepository
        :BaseInsertRepository<CreateProductSaleUnitDto>,
            ICreateProductSaleUnitRepository
    {
        public CreateProductSaleUnitRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {
        }

        protected override async Task<CreateProductSaleUnitDto> InsertInternal(
            CreateProductSaleUnitDto dto)
        {
            Guid productSaleUnitId = await InsertInProductSaleUnits(dto);

            return dto with
            {
                ProductSaleUnitId = productSaleUnitId
            };
        }

        protected override void HandlePrevalidations(CreateProductSaleUnitDto dto)
        {
            if (AbbreviationExist(dto.Abbreviation))
            {
                throw new CreateProductSaleUnitArgumentException(
                    $"La abreviatura {dto.Abbreviation} ya existe."
                    );
            }
        }

        #region :: Prevalidations ::

        private bool AbbreviationExist(
            string abbreviation)
        {
            bool result = false;
            
            string sql = @"
                select  * 
                from    product_sale_units
                where   abbreviation =@abbreviation
            ";

            IEnumerable<Models.Entities.ProductSaleUnits> productSaleUnits;
            productSaleUnits = Connection
                .Query<Models.Entities.ProductSaleUnits>(
                    sql,
                    new
                    {
                        abbreviation = abbreviation
                    });

            if (productSaleUnits.Any())
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region :: Create Product Sale Unit Steps ::

        private async Task<Guid> InsertInProductSaleUnits(
            CreateProductSaleUnitDto dto)
        {
            Guid productSaleUnitId = Guid.Empty;
            
            string sql = @"
                insert into product_sale_units 
                    (
                     product_sale_unit_id,
                     abbreviation,
                     description,
                     company_id
                    ) 
                    values 
                    (
                     @product_sale_unit_id,
                     @abbreviation,
                     @description,
                     @company_id         
                    ) 
                returning product_sale_unit_id;
            ";

            productSaleUnitId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_sale_unit_id = Guid.NewGuid(),
                        abbreviation = dto.Abbreviation,
                        description = dto.Description,
                        company_id = dto.CompanyId,
                    });

            return productSaleUnitId;
        }

        #endregion     
    }
}