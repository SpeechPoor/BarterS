using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public  class shoppingManager
    {
        #region 查询所有购物车信息
        /// <summary>
        /// 查询所有购物车信息
        /// </summary>
        /// <returns></returns>
        public static List<shoppingInfo> GetShoppingInfos()
        {
            return shoppingService.GetShoppingInfos();
        }
        #endregion

        #region 添加购物车
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <returns></returns>
        public static int shoppingAdd(shoppingInfo shopping)
        { 
            return shoppingService.shoppingAdd(shopping);
        }
        #endregion

        #region 根据ID删除购物车信息
        /// <summary>
        /// 根据ID删除购物车信息
        /// </summary>
        /// <param name="Proid"></param>
        /// <returns></returns>
        public static int shoppingDelete(int Proid)
        { 
            return shoppingService.shoppingDelete(Proid);
         }
        #endregion

    }
}
