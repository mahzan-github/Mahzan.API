using System;

namespace Mahzan.Persistance.V1.Exeptions.Products.CreateProduct
{
    public class CreateProductArgumentException:ArgumentException
    {
        public CreateProductArgumentException(string message) : base(message)
        {
        }
    }
}