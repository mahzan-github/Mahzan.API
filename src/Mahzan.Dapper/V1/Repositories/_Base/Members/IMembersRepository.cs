using Mahzan.Dapper.V1.Filters._Base.Members;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories._Base.Members
{
    public interface IMembersRepository
    {
        Models.Entities.Members GetMemberByFilter(GetMemberFilter filter);
    }
}
