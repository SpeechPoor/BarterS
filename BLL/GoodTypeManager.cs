using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
namespace BLL
{
    public class GoodTypeManager
    {
        public static List<GoodType> GetGoodType()
        {
            return GoodTypeService.GetGoodType();
        }
    }
}
