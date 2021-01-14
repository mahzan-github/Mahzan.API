using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Models.Entities
{
    public class TaxRegimeCodes
    {
        public Guid TaxRegimeCodeId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool MoralPerson { get; set; }

        public bool PhysicalPerson { get; set; }
    }
}
