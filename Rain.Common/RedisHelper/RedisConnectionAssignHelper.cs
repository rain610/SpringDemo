using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Common.RedisHelper
{
    public class RedisConnectionAssignHelper
    {
        private static ConnectionMultiplexer _redis;
        private static IDatabase _db;
        private static IServer _server;
        private static bool needSave = false;
        private void Init(string host, int port, string pwd, int database)
        {
            var options = ConfigurationOptions.Parse(host + ":" + port);
            options.SyncTimeout = int.MaxValue;
            options.AllowAdmin = true;
            if (!string.IsNullOrEmpty(pwd))
            {
                options.Password = pwd;
            }
            if (_redis == null)
                _redis = ConnectionMultiplexer.Connect(options);
            if (_server == null)
                _server = _redis.GetServer(host + ":" + port);
            if (_db == null)
                _db = _redis.GetDatabase(database);
            needSave = false;
        }

        //public static bool HasShopUser(string userName)
        //{
        //    bool hasUser = false;
        //    ShopUserEntity userEntity;
        //    userEntity = null;
        //    using (RedisHelper redis = new RedisHelper(HOST, PORT, PWD))
        //    {
        //        userEntity = redis.GetShopUserInfo(userName);
        //    }

        //    if (userEntity != null)
        //    {
        //        hasUser = true;
        //    }
        //    return hasUser;
        //}
    }
}
