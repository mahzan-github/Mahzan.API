using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductDepartments;
using Mahzan.Persistance.V1.Exeptions.ProductDepartments.CreateProductDepartment;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductDepartments.CreateProductDepartment
{
    public class CreateProductDepartment:BaseInsertRepository<CreateProductDepartmentDto>,
    ICreateProductDepartment
    {
        public CreateProductDepartment(
            NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<CreateProductDepartmentDto> InsertInternal(
            CreateProductDepartmentDto dto)
        {
            Guid productDepartmentId = await
                InsertInProuctDepartments(dto);

            return dto with
            {
                ProductDepartmentId = productDepartmentId
            };
        }

        protected override async Task HandlePrevalidations(CreateProductDepartmentDto dto)
        {
            if (DepartmentNameExist(dto.CompanyId,dto.Name))
            {
                throw new CreateProductDepartmentArgumentException(
                    $"El nombre {dto.Name} para el departamento ya existe."
                );
            }
            
            if (!CompanyIdExist(dto.CompanyId))
            {
                throw new CreateProductDepartmentArgumentException(
                    $"El company {dto.CompanyId} no existe."
                );
            }
        }

        #region :: Prevalidations ::

        private bool DepartmentNameExist(
            Guid companyId,
            string name)
        {
            bool result = false;
            
            string sql = @"
                select  * 
                from    product_departments
                where   company_id = @company_id
                and     name = @name
            ";

            IEnumerable<Models.Entities.ProductDepartments> productDepartments;
            productDepartments = Connection
                .Query<Models.Entities.ProductDepartments>(
                    sql,
                    new
                    {
                        company_id = companyId,
                        name = name
                    });

            if (productDepartments.Any())
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
        
        #region :: Create Product Department Steps ::

        private async Task<Guid> InsertInProuctDepartments(
            CreateProductDepartmentDto dto)
        {
            Guid productDepartmentId = Guid.Empty;
            
            string sql = @"
                insert into product_departments
                    (
                     product_department_id,
                     code_department,
                     name,
                     company_id
                    )
                    values
                    (
                     @product_department_id,
                     @code_department,
                     @name,
                     @company_id         
                    )
                    returning product_department_id;
            ";

            productDepartmentId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        product_department_id = Guid.NewGuid(),
                        code_department = dto.CodeDepartment,
                        name = dto.Name,
                        company_id = dto.CompanyId
                    }
                );

            return productDepartmentId;
        }

        #endregion
    }
}