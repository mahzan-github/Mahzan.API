using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductCategories;
using Mahzan.Persistance.V1.Exeptions.ProductCategories.CreateProductCategory;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductCategories.CreateProductCategory
{
    public class CreateProductCategoryRepository: BaseInsertRepository<CreateProductCategoryDto>,
        ICreateProductCategoryRepository
    {
        public CreateProductCategoryRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {
        }

        protected override async Task<CreateProductCategoryDto> InsertInternal(
            CreateProductCategoryDto dto)
        {
            Guid productCategoryId = await InsetInProductCategories(dto);

            return dto with
            {
                ProductCategoryId = productCategoryId
            };
        }

        protected override void HandlePrevalidations(CreateProductCategoryDto dto)
        {
            if (CategoryDescriptionExist(dto.CompanyId,dto.Description))
            {
                throw new CreateProductCategoryArgumentException(
                    $"La descripción {dto.Description} para una categoría ya existe."
                );
            }
            
            if (!CompanyIdExist(dto.CompanyId))
            {
                throw new CreateProductCategoryArgumentException(
                    $"El company {dto.CompanyId} no existe."
                );
            }
        }

        #region :: Prevalidations ::

        private bool CategoryDescriptionExist(
            Guid companyId,
            string description)
        {
            bool result = false;
            
            string sql = @"
                select  * 
                from    product_catagories
                where   company_id = @company_id
                and     description = @description
            ";

            IEnumerable<Models.Entities.ProductCategories> productCategories;
            productCategories = Connection
                .Query<Models.Entities.ProductCategories>(
                    sql,
                    new
                    {
                        company_id = companyId,
                        description = description
                    });

            if (productCategories.Any())
            {
                result = true;
            }

            return result;
        }
        
        private bool CompanyIdExist(
            Guid companyId)
        {
            bool result = false;
            
            string sql = @"
                select  * 
                from    companies
                where   company_id = @company_id
            ";

            IEnumerable<Models.Entities.Companies> companies;
            companies = Connection
                .Query<Models.Entities.Companies>(
                    sql,
                    new
                    {
                        company_id = companyId,
                    });

            if (companies.Any())
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region :: Create Product Category Steps ::

        private async Task<Guid> InsetInProductCategories(CreateProductCategoryDto dto)
        {
            Guid productCategoryId = Guid.Empty;
            
            string sql = @"
                insert into product_catagories
                (
                 product_catagory_id,
                 code_category,
                 description,
                 company_id
                )
                values
                (
                 @product_catagory_id,
                 @code_category,
                 @description,
                 @company_id
                )
                returning product_catagory_id;
            ";

            productCategoryId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_catagory_id= Guid.NewGuid(),
                        code_category = dto.CodeCategory,
                        description = dto.Description,
                        company_id = dto.CompanyId
                    });

            return productCategoryId;
        }

        #endregion
    }
}