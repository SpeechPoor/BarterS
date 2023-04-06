
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class SqlDbHelper
    {
        static string conStr = ConfigurationManager.AppSettings["conStr"];

        static SqlConnection con = null;


        #region 获取连接的方法
        /// <summary>
        /// 获取连接的方法
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {

            if (con == null || con.ConnectionString == "")
            {
                con = new SqlConnection(conStr);
            }

            return con;
        }
        #endregion

        #region 打开连接方法
        public static void OpenCon()
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
        }
        #endregion

        #region 关闭连接方法
        public static void CloseCon()
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region 执行一般查询：返回多行多列
        /// <summary>
        /// 执行一般查询：返回多行多列  select
        /// </summary>
        /// <param name="sql">SQL指令或者存储过程</param>
        /// <param name="type">命令的类型</param>
        /// <param name="para">参数列表</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql,//SQL指令或者存储过程
            CommandType type = CommandType.Text,//命令的类型：SQL文本或者存储过程  默认SQL文本
            params SqlParameter[] para)//参数列表
        {
            SqlConnection con = GetConnection();//初始化连接对象
            OpenCon();//打开连接
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = type;//设置集合类型
            com.Parameters.AddRange(para);//初始SQL指令中的参数
            SqlDataReader dr = com.ExecuteReader();//执行查询
            return dr;
        }
        #endregion

        #region 执行动作查询:添加、修改、删除
        /// <summary>
        /// 执行动作查询：添加(Insert)、修改(Update)、删除(Delete)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,//SQL指令或者存储过程
            CommandType type = CommandType.Text,//命令的类型：SQL文本或者存储过程  默认SQL文本
            params SqlParameter[] para)//参数列表
        {
            int n = 0;
            SqlConnection con = GetConnection();//初始化连接对象
            OpenCon();//打开连接
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = type;//设置集合类型
            com.Parameters.AddRange(para);//初始SQL指令中的参数
            n = com.ExecuteNonQuery();//执行动作查询
            CloseCon();//关闭连接
            return n;
        }
        #endregion

        #region 根据用户提供的SQL查询返回一个DataTable对象(临时表数据集合)
        public static DataTable GetTable(string sql,//SQL指令或者存储过程
            CommandType type = CommandType.Text,//命令的类型：SQL文本或者存储过程  默认SQL文本
            params SqlParameter[] para)//参数列表
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, GetConnection());
            sda.SelectCommand.CommandType = type;
            sda.SelectCommand.Parameters.AddRange(para);
            sda.Fill(dt);
            return dt;
        }

        internal static int ExecuteNonQuery(object text, SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
