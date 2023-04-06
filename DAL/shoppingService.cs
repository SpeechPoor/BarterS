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
    public  class shoppingService
    {
     
        #region 查询所有购物车信息
        /// <summary>
        /// 查询所有购物车信息
        /// </summary>
        /// <returns></returns>
        public static List<shoppingInfo> GetShoppingInfos()
        { 
            List<shoppingInfo> list = new List<shoppingInfo>();
            string sql = " select * from shopping_trolley";
            SqlDataReader dr=SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            { 
                shoppingInfo si = new shoppingInfo();
                si.shoppingId = (int)dr["shopping_Id"];
                si.ProPicture=dr["shopping_picture"].ToString();
                si.ProId = (int)dr["shopping_ProId"];
                si.ProName = dr["shopping_ProName"].ToString();
                si.TypeId = (int)dr["Type_ID"];
                si.ProPrice = Convert.ToDouble(dr["shopping_ProPrice"]);
                list.Add(si);
            }
            SqlDbHelper.CloseCon();
            return list;
        }
        #endregion

        #region 添加购物车
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <returns></returns>
        public static int shoppingAdd(shoppingInfo shopping)
        {
            int n = 0;
            string sql = "insert shopping_trolley(shopping_picture, shopping_ProId, shopping_ProName, Type_ID, shopping_ProPrice)";
            sql += "values(@shopping_picture,@shopping_ProId,@shopping_ProName,@Type_ID,@shopping_ProPrice)";
            n = SqlDbHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter[] {
                    new SqlParameter("@shopping_picture",shopping.ProPicture),
                    new SqlParameter("@shopping_ProId",shopping.ProId),
                    new SqlParameter("@shopping_ProName",shopping.ProName),
                    new SqlParameter("@Type_ID",shopping.TypeId),
                    new SqlParameter("@shopping_ProPrice",shopping.ProPrice),
                }); 
            return n;
        }
        #endregion

        #region 根据ID删除购物车信息
        /// <summary>
        /// 根据ID删除购物车信息
        /// </summary>
        /// <returns></returns>
        public static int shoppingDelete(int Proid)
        {
            int n = 0;
            string sql = "delete shopping_trolley where shopping_Id="+ Proid + "";
            n=SqlDbHelper.ExecuteNonQuery(sql);
            return n;
        }
        #endregion
    }
}

