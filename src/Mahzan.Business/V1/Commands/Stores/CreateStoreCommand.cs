using System;

namespace Mahzan.Business.V1.Commands.Stores
{
    public class CreateStoreCommand
    {
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public Guid MemberId { get; set; }
    }
}