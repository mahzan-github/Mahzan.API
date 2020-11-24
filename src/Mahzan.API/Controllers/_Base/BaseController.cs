using Mahzan.Dapper.V1.Filters._Base.Members;
using Mahzan.Dapper.V1.Repositories._Base.Members;
using Mahzan.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahzan.API.Controllers._Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMembersRepository _membersRepository;

        public BaseController(
            IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public string UserName 
        {
            get 
            {
                return string.Empty;
            }
        }

        public Guid MemberId
        {
            get
            {
                Members member = _membersRepository
                    .GetMemberByFilter(new GetMemberFilter
                    {
                        UserName= HttpContext.User.Claims.ToList()[0].Value
                    });

                return member.MemberPatternId == null ? member.MemberId : member.MemberPatternId.Value;
            }
        }
    }
}
