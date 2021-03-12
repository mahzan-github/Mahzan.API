using System;

namespace Mahzan.Persistance.V1.Exeptions.ProductPurchaseUnits.CreateProductPurchaseUnit
{
    public class CreateProductPurchaseUnitArgumentException:ArgumentException
    {
        public CreateProductPurchaseUnitArgumentException(string message) : base(message)
        {
        }
    }
}