using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rain.BaseRepository;
using Rain.Common;
using Rain.Common.RedisHelper;
using Rain.Entities;

namespace Rain.Repository
{
    public class AddressRepository
    {
        public List<AddressEntity> List()
        {
            //RedisHelper redisHelper = new RedisHelper();
            //var username = "Rain";
            //if (!Cache.Exists("username"))
            //{
            //    Cache.Insert("username", username);
            //}
            var context = RainDbContext.GetContext();
            var test = (from t in context.Set<AddressEntity>()
                        select t).ToList().Take(10);
            //if (redisHelper.KeyExists("addressList"))
            //{
            //    var list = JsonConvert.DeserializeObject<List<AddressEntity>>(redisHelper.StringGet("addressList"));
            //    TimeSpan timeSpan = new TimeSpan(0, 1, 0);
            //    redisHelper.StringSet("addressList", JsonConvert.SerializeObject(test), timeSpan);
            //}
            //else 
            //{
            //    TimeSpan timeSpan = new TimeSpan(0,1,0);
            //    redisHelper.StringSet("addressList", JsonConvert.SerializeObject(test), timeSpan);
            //}
            


            return test.ToList();
        }
    }
}
