using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UserInfoService
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
            List<UserInfo> list = new List<UserInfo>();
            string sql = "select * from User_Info  where User_Login ='"+ Ulogin  + "'";
            SqlDataReader dr=SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            { 
                UserInfo us = new UserInfo();
                us.Uid = (int)dr["User_ID"];
                us.Ulogin=dr["User_Login"].ToString();
                us.Upass=dr["User_Pass"].ToString() ;
                us.Uphone = dr["User_Phone"].ToString();
                us.Uname = dr["User_Name"].ToString();
                us.Ustatus = dr["User_Status"].ToString();
                list.Add(us);
            }    
            SqlDbHelper.CloseCon();
            dr.Close();
            return list;
        }
        #endregion

        #region 注册用户账号
        /// <summary>
        /// 注册用户账号
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int InsertUserInfo(UserInfo u)
        {
            int n = 0;
            string sql = "insert into User_Info values(@User_ID,@User_Login,@User_Pass,@User_Phone,@User_Name,@User_Status)";
            n = SqlDbHelper.ExecuteNonQuery(sql, CommandType.StoredProcedure, new SqlParameter[]
            {
                new SqlParameter("@User_ID",u.Uid),
                new SqlParameter(",@User_Login",u.Ulogin),
                new SqlParameter("@User_Pass",u.Upass),
                new SqlParameter("@User_Phone",u.Uphone),
                new SqlParameter("@User_Name",u.Uname),
                new SqlParameter("@User_Status",u.Ustatus),

            });
            return n;
        }
        #endregion

    }
}
