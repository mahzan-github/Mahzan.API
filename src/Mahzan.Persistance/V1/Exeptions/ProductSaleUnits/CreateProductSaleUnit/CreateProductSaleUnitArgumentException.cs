using System;

namespace Mahzan.Persistance.V1.Exeptions.ProductSaleUnits.CreateProductSaleUnit
{
    public class CreateProductSaleUnitArgumentException:ArgumentException
    {
        public CreateProductSaleUnitArgumentException(string message) : base(message)
        {
        }
    }
}