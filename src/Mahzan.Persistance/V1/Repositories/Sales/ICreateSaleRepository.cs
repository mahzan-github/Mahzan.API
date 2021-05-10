using Mahzan.Persistance.V1.Dto.ProductTaxes;
using Mahzan.Persistance.V1.Dto.Sales;
using Mahzan.Persistance.V1.Repositories._Base;

namespace Mahzan.Persistance.V1.Repositories.Sales
{
    public interface ICreateSaleRepository
        :IBaseInsertRepository<CreateSaleDto>
    {
        
    }
}