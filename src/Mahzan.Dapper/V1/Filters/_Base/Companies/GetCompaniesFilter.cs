using Mahzan.Dapper.V1.Rules.Filters._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.Filters._Base.Companies
{
    public class GetCompaniesFilter:FilterBase
    {
        public Guid CompanyId { get; set; }
    }
}
