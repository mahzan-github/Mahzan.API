using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.Stores;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.Stores
{
    public class CreateStoreRepository
        :BaseInsertRepository<CreateStoreDto>, ICreateStoreRepository
    {
        public CreateStoreRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<CreateStoreDto> InsertInternal(CreateStoreDto dto)
        {
            Guid storeId = await InsertInStores(dto);
            
            return dto with
            {
                StoreId = storeId
            };
        }
        
        protected override void HandlePrevalidations(CreateStoreDto dto)
        {
            //TODO: Validar que la tienda no exista para este miembro
        }
        
        #region :: Insert Store Steps ::

        private async Task<Guid> InsertInStores(CreateStoreDto dto)
        {
            Guid storeId = Guid.Empty;

            string sql = @"
                insert into stores
                (
                    store_id,
                    name,
                    member_id
                )
                values
                (
                    @store_id,
                    @name,
                    @member_id
                )
                returning store_id;
            ";

            storeId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        store_id = Guid.NewGuid(),
                        name = dto.Name,
                        member_id = dto.MemberId
                    });
            return storeId;
        }

        #endregion
    }
}