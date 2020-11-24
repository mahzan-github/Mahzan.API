using Mahzan.Dapper.V1.Rules.Filters._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.Rules.Filters.Companies
{
    public class CompanyExistFilter:FilterBase
    {
        public string RFC { get; set; }
    }
}
