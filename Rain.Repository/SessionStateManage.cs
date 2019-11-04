using Rain.Common.RedisHelper;
using Rain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rain.Repository
{
    public class SessionStateManage
    {
        #region Property
        public static string CookieName { get; private set; }
        public static string ApplicationName { get; private set; }
        public static int SessionTimeOut { get; private set; }
        #endregion
        static RedisHelper redisHelper;
        static SessionStateManage()
        {
            CookieName = ConfigurationManager.AppSettings["cookieName"] ?? "SESSION_ID";
            ApplicationName = ConfigurationManager.AppSettings["ApplicationName"] ?? "SESSION";

            redisHelper = new RedisHelper();
            redisHelper.SetSysCustomKey(ApplicationName);
            string sessionTimeOutStr = ConfigurationManager.AppSettings["SessionTimeOut"] ?? "20";
            SessionTimeOut = int.Parse(sessionTimeOutStr);
        }

        public static void SetLogin(string cookieName, LoginVisualEntity user)
        {
            redisHelper.StringSet(cookieName, user.UserName);
            redisHelper.KeyExpire(cookieName, new TimeSpan(0, SessionTimeOut, 0));

            redisHelper.HashSet(user.UserName, user);
        }

        public static LoginVisualEntity GetUser(string cookinNme)
        {
            string userID = redisHelper.StringGet(cookinNme);
            return userID == null ? null : redisHelper.HashGet<LoginVisualEntity>(userID);
        }
    }
}
