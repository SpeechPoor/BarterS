using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class UserInfoManager
    {
        #region 根据账号密码是否一致登录
        /// <summary>
        /// 根据账号密码是否一致登录
        /// </summary>
        /// <param name="Ulogin"></param>
        /// <param name="Upass"></param>
        /// <returns></returns>
        public static List<UserInfo> GetUserinfo(string Ulogin)
        { 
            return UserInfoService.GetUserinfo(Ulogin);
        }
        #endregion

        #region  注册用户账号
        /// <summary>
        /// 注册用户账号
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int InsertUserInfo(UserInfo u)
        { 
            return UserInfoService.InsertUserInfo(u);
        }
        #endregion
    }
}
