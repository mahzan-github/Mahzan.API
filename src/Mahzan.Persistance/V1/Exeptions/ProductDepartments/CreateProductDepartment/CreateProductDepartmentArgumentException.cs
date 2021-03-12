using System;

namespace Mahzan.Persistance.V1.Exeptions.ProductDepartments.CreateProductDepartment
{
    public class CreateProductDepartmentArgumentException:ArgumentException
    {
        public CreateProductDepartmentArgumentException(string message) : base(message)
        {

        }
    }
}