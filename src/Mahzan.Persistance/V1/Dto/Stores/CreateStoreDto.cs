using System;

namespace Mahzan.Persistance.V1.Dto.Stores
{
    public record CreateStoreDto
    {
        public Guid StoreId { get; set; }

        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public Guid MemberId { get; set; }
    }
}