using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class GoodsInfoManager
    {
        #region 查询全部商品
        /// <summary>
        /// 查询全部商品
        /// </summary>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsInfos()
        { 
            return GoodsInfoService.GetGoodsInfos();
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
            return GoodsInfoService.ClassifyGoodsInfo(Gtype);
        }
        #endregion

        #region 根据ID查询商品
        /// <summary>
        /// 根据ID查询商品
        /// </summary>
        /// <param name="Gid"></param>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsInfoGids(int Gid)
        {
            return GoodsInfoService.GetGoodsInfoGids(Gid);
        }
        #endregion

        #region 发布闲置
        /// <summary>
        /// 发布闲置
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public static int GoodsAdd(GoodsInfo good)
        {
            return GoodsInfoService.GoodsAdd(good);
        }
        #endregion

    }
}
