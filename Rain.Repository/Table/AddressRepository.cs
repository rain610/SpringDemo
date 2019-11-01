using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rain.BaseRepository;
using Rain.Common;
using Rain.Entities;

namespace Rain.Repository
{
    public class AddressRepository
    {
        public List<AddressEntity> List()
        {
            var username = "Rain";
            if (Cache.Exists("username"))
            {
                Cache.Insert("username", username);
            }
            var context = RainDbContext.GetContext();
            var test = (from t in context.Set<AddressEntity>()
                        select t).ToList();
            return test;
        }
    }
}
