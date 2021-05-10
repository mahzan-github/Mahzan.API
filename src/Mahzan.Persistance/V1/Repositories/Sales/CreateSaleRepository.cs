using System.Threading.Tasks;
using Mahzan.Persistance.V1.Dto.Sales;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Sales
{
    public class CreateSaleRepository
        :BaseInsertRepository<CreateSaleDto>,
            ICreateSaleRepository
    {
        public CreateSaleRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override Task<CreateSaleDto> InsertInternal(CreateSaleDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}