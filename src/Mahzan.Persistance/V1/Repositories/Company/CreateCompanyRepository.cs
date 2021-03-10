using System.Threading.Tasks;
using Mahzan.Persistance.V1.Dto.Company;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Company
{
    public class CreateCompanyRepository:BaseRepository<CreateCompanyDto>,
        ICreateCompanyRepository
    {
        public CreateCompanyRepository(
            NpgsqlConnection connection) 
            : base(connection)
        {
            
        }

        protected override Task<CreateCompanyDto> InsertInternal(CreateCompanyDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}