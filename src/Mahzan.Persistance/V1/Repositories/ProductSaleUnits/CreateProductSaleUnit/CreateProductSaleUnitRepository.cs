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
            throw new System.NotImplementedException();
        }

        protected override async Task HandlePrevalidations(CreateProductSaleUnitDto dto)
        {
            if (await AbbreviationExist(dto.Abbreviation))
            {
                throw new CreateProductSaleUnitArgumentException(
                    $"La abreviatura {dto.Abbreviation} ya existe."
                    );
            }
        }

        #region :: Prevalidations ::

        private async Task<bool> AbbreviationExist(
            string abbreviation)
        {
            bool result = false;
            
            string sql = @"
                select  * 
                from    product_sale_units
                where   abbreviation =@abbreviation
            ";

            IEnumerable<Models.Entities.ProductSaleUnits> productSaleUnits;
            productSaleUnits = await Connection
                .QueryAsync<Models.Entities.ProductSaleUnits>(
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

        #region :: Create Product Purchase Unit Steps ::

        private async Task<Guid> InsertInProductPurchaseUnits(
            CreateProductPurchaseUnitDto dto)
        {
            Guid productPurchaseUnitId = Guid.Empty;
            
            string sql = @"
                insert into product_purchase_units 
                    (
                     product_purchase_unit_id,
                     abbreviation,
                     description,
                     company_id
                    ) 
                    values 
                    (
                     @product_purchase_unit_id,
                     @abbreviation,
                     @description,
                     @company_id         
                    ) 
                returning product_purchase_unit_id;
            ";

            productPurchaseUnitId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_purchase_unit_id = Guid.NewGuid(),
                        abbreviation = dto.Abbreviation,
                        description = dto.Description,
                        company_id = dto.CompanyId,
                    });

            return productPurchaseUnitId;
        }

        #endregion     
    }
}