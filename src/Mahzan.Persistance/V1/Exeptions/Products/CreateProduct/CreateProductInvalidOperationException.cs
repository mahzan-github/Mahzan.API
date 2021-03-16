using System;

namespace Mahzan.Persistance.V1.Exeptions.Products.CreateProduct
{
    public class CreateProductInvalidOperationException:InvalidOperationException
    {
        public CreateProductInvalidOperationException(string message) : base(message){
        }
    }
}