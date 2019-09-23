using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.BaseEntities
{
    public class MappingBase<TEntity> : EntityTypeConfiguration<TEntity>, IMapping
        where TEntity : class
    {
        #region IMapping 成员

        public void RegistTo(ConfigurationRegistrar configurationRegistrar)
        {
            configurationRegistrar.Add(this);
        }

        #endregion
    }
}
