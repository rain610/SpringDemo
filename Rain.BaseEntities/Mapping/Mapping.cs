using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.BaseEntities
{
    public class Mapping<TEntity> : MappingBase<TEntity>, IMapping
        where TEntity : BaseEntity, new()
    {
        public Mapping()
        {
            HasKey(x => x.ID);
        }
    }
}
