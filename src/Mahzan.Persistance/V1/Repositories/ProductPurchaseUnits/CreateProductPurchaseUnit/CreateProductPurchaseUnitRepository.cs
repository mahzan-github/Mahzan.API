using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductPurchaseUnits;
using Mahzan.Persistance.V1.Exeptions.ProductPurchaseUnits.CreateProductPurchaseUnit;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductPurchaseUnits.CreateProductPurchaseUnit
{
    public class CreateProductPurchaseUnitRepository
    :BaseInsertRepository<CreateProductPurchaseUnitDto>,
        ICreateProductPurchaseUnitRepository
    {
        public CreateProductPurchaseUnitRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<CreateProductPurchaseUnitDto> InsertInternal(
            CreateProductPurchaseUnitDto dto)
        {
            Guid productPurchaseUnitId = await InsertInProductPurchaseUnits(dto);

            return dto with
            {
                ProductPurchaseUnitId = productPurchaseUnitId
            };
        }

        protected override async Task HandlePrevalidations(CreateProductPurchaseUnitDto dto)
        {
            if (await AbbreviationExist(dto.Abbreviation))
            {
                throw new CreateProductPurchaseUnitArgumentException(
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
                from    product_purchase_units
                where   abbreviation =@abbreviation
            ";

            IEnumerable<Models.Entities.ProductPurchaseUnits> productPurchaseUnits;
            productPurchaseUnits = await Connection
                .QueryAsync<Models.Entities.ProductPurchaseUnits>(
                    sql,
                    new
                    {
                        abbreviation = abbreviation
                    });

            if (productPurchaseUnits.Any())
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