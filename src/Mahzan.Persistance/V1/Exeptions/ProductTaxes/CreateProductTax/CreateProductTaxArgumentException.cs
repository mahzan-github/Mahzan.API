using System;

namespace Mahzan.Persistance.V1.Exeptions.ProductTaxes.CreateProductTax
{
    public class CreateProductTaxArgumentException:ArgumentException
    {
        public CreateProductTaxArgumentException(string message) : base(message)
        {
        }
    }
}