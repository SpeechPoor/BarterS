using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class GoodTypeService
    {
        #region 获取所有商品类型
        /// <summary>
        /// 获取所有商品类型
        /// </summary>
        /// <returns></returns>
        public static List<GoodType> GetGoodType()
        {
            List<GoodType> types = new List<GoodType>();
            string sql = "select * from Used_Goods_Type";
            SqlDataReader dr = SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                GoodType type = new GoodType();
                type.ID = Convert.ToInt32(dr["Type_ID"]);
                type.Name = dr["Type_Name"].ToString();
                type.Description = (dr["Type_Description"]).ToString();
                types.Add(type);
            }
            return types;
        }
        #endregion
    }
}
