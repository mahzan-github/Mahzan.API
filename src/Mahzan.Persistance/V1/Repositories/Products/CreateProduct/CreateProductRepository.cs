using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.Products;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.Repositories.ProductCategories.CreateProductCategory;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Products.CreateProduct
{
    public class CreateProductRepository:BaseInsertRepository<CreateProductDto>,
        ICreateProductRepository
    {
        public CreateProductRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {
        }

        protected override async Task<CreateProductDto> InsertInternal(CreateProductDto dto)
        {
            Guid productId = await InsertInProducts(dto);

            return dto with
            {
                ProductId = productId
            };
        }

        protected override void HandlePrevalidations(CreateProductDto dto)
        {
            base.HandlePrevalidations(dto);
        }

        #region :: Prevalidations ::

        //TODO:Si la cetgoria viene validar que exista
        
        //TODO:Si el departemanto viene validar que exista
        
        //TODO:Si el precio de compra viene validar que exista
        
        //TODO:Si el precio de venta viene validar que exista
        
        //TODO:Validar que la compañia exista

        #endregion
        
        #region :: Create Product Steps ::

        private async Task<Guid> InsertInProducts(CreateProductDto dto)
        {
            Guid productId = Guid.Empty;
            
            string sql = @"
                insert into products
                (
                 product_id,
                 key_code,
                 key_alternative_code,
                 description,
                 product_catagory_id,
                 product_department_id,
                 product_purchase_unit_id,
                 product_sale_unit_id,
                 factor,
                 company_id
                ) 
                values 
               (
                 @product_id,
                 @key_code,
                 @key_alternative_code,
                 @description,
                 @product_catagory_id,
                 @product_department_id,
                 @product_purchase_unit_id,
                 @product_sale_unit_id,
                 @factor,
                 @company_id       
               )
               returning product_id;
            ";

            productId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_id = Guid.NewGuid(),
                        key_code = dto.KeyCode,
                        key_alternative_code = dto.KeyAlternativeCode,
                        description = dto.Description,
                        product_catagory_id = dto.ProductCatagoryId,
                        product_department_id = dto.ProductDepartmentId,
                        product_purchase_unit_id = dto.ProductPurchaseUnitId,
                        product_sale_unit_id = dto.ProductSaleUnitId,
                        factor = dto.Factor,
                        company_id = dto.CompanyId
                    });

            return productId;
        }

        #endregion
    }
}