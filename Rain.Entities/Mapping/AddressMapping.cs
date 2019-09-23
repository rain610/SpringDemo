using Rain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Entities.Mapping
{
    public class AddressMapping: MappingBase<AddressEntity>
    {
        public AddressMapping()
        {
            this.HasKey(x => new { x.AddressID });
        }
    }
}
