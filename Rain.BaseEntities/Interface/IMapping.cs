using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.BaseEntities
{
    public interface IMapping
    {
        void RegistTo(ConfigurationRegistrar configurationRegistrar);
    }
}
