using Rain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Entities.Mapping
{
    public class FileMapping : MappingBase<DocumentCenterFileEntity>
    {
        public FileMapping()
        {
            this.HasKey(x => new { x.FileCode });
        }
    }
}
