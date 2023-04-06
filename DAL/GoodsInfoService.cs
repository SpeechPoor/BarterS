using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;

namespace DAL
{
    public class GoodsInfoService
    {

        #region 查询全部商品
        /// <summary>
        /// 查询全部商品
        /// </summary>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsInfos()
        { 
            List<GoodsInfo> goodsInfo = new List<GoodsInfo>();
            string sql = "select * from Used_Goods_Info";
            SqlDataReader dr=SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            { 
                GoodsInfo Gi = new GoodsInfo();
                Gi.Gid = (int)dr["Goods_ID"];
                Gi.Gname=dr["Goods_Name"].ToString();
                Gi.Gtype = (int)dr["Goods_Type_ID"];
                Gi.Gprice = Convert.ToDouble(dr["Goods_Price"]);
                Gi.Gseller =(int)dr["Goods_Seller"];
                Gi.Gstatus = Convert.ToBoolean( dr["Goods_Status"]);
                Gi.Gdescription=dr["Goods_Description"].ToString();
                Gi.GDateTime = (DateTime)dr["Goods_Date"];
                Gi.Gpicture = dr["Goods_picture"].ToString();
                goodsInfo.Add(Gi);
            }
            dr.Close();
            SqlDbHelper.CloseCon();
            return goodsInfo;
        }
        #endregion

        #region 根据商品类型查询商品
        /// <summary>
        /// 根据商品类型查询商品
        /// </summary>
        /// <param name="Gtype"></param>
        /// <returns></returns>
        public static List<GoodsInfo> ClassifyGoodsInfo(int Gtype)
        { 
            List<GoodsInfo> list = new List<GoodsInfo>();
            string sql = "select * from Used_Goods_Info where Goods_Type_ID =  " + Gtype + "";
            SqlDataReader dr = SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                GoodsInfo Gi = new GoodsInfo();
                Gi.Gid = (int)dr["Goods_ID"];
                Gi.Gname = dr["Goods_Name"].ToString();
                Gi.Gtype = (int)dr["Goods_Type_ID"];
                Gi.Gprice = Convert.ToDouble(dr["Goods_Price"]);
                Gi.Gseller = (int)dr["Goods_Seller"];
                Gi.Gstatus = Convert.ToBoolean(dr["Goods_Status"]);
                Gi.Gdescription = dr["Goods_Description"].ToString();
                Gi.GDateTime = (DateTime)dr["Goods_Date"];
                Gi.Gpicture = dr["Goods_picture"].ToString();
                list.Add(Gi);
            }
            dr.Close();
            SqlDbHelper.CloseCon();
            return list;
        }
        #endregion       

        #region 根据ID查询商品
        /// <summary>
        /// 根据ID查询商品
        /// </summary>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsInfoGids(int Gid)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            string sql = "select * from Used_Goods_Info where Goods_ID =  " + Gid + "";
            SqlDataReader dr = SqlDbHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                GoodsInfo Gi = new GoodsInfo();
                Gi.Gid = (int)dr["Goods_ID"];
                Gi.Gname = dr["Goods_Name"].ToString();
                Gi.Gtype = (int)dr["Goods_Type_ID"];
                Gi.Gprice = Convert.ToDouble(dr["Goods_Price"]);
                Gi.Gseller = (int)dr["Goods_Seller"];
                Gi.Gstatus = Convert.ToBoolean(dr["Goods_Status"]);
                Gi.Gdescription = dr["Goods_Description"].ToString();
                Gi.GDateTime = (DateTime)dr["Goods_Date"];
                Gi.Gpicture = dr["Goods_picture"].ToString();
                list.Add(Gi);
            }
            dr.Close();
            SqlDbHelper.CloseCon();
            return list;
        }
        #endregion

        #region 发布闲置
        /// <summary>
        /// 发布闲置
        /// </summary>
        /// <returns></returns>                                             
        public static int GoodsAdd(GoodsInfo good)
        {
            int n = 0;
            string sql = "insert into Used_Goods_Info values(@Goods_Name, @Goods_Type_ID, " +
                "@Goods_Price, @Goods_Seller, @Goods_Status, @Goods_Description, @Goods_Date, @Goods_picture)";
            n = SqlDbHelper.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter[]{
                    new SqlParameter("@Goods_Name",good.Gname),
                    new SqlParameter("@Goods_Type_ID",good.Gtype),
                    new SqlParameter("@Goods_Price",good.Gprice),
                    new SqlParameter("@Goods_Seller",good.Gseller),
                    new SqlParameter("@Goods_Status",good.Gstatus),
                    new SqlParameter("@Goods_Description",good.Gdescription),
                    new SqlParameter("@Goods_Date",good.GDateTime),
                    new SqlParameter("@Goods_picture",good.Gpicture)
                }
                );
            if (n > 0)
            {
                MessageBox.Show("添加成功");
            }

            return n;
        }
        #endregion
    }
}
