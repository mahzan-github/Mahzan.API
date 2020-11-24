using Mahzan.Dapper.V1.Filters._Base.Members;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Mahzan.Dapper.V1.Repositories._Base.Members
{
    public class MembersRepository : DataConnection,IMembersRepository
    {
        public MembersRepository(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public Models.Entities.Members GetMemberByFilter(GetMemberFilter filter)
        {
            Models.Entities.Members member= null;

            DynamicParameters parameters = new DynamicParameters();

            string sql = @"
                select * 
                from members
                inner join users on users.user_id = members.user_id
                where 1=1
            ";

            if (filter.UserName!=null)
            {
                sql += "and users.user_name = @user_name";
                parameters.Add("@user_name", filter.UserName, DbType.String);
            }

            IEnumerable<Models.Entities.Members> members;
            members = Connection
                .Query<Models.Entities.Members >(
                    sql,
                    parameters
                );

            if (members.Any())
            {
                member = members.ToList().FirstOrDefault();
            }


            return member;
        }
    }
}
