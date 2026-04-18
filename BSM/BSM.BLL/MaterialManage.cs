using BSM.DAL;
using BSM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.BLL
{
    public class MaterialManage
    {
        // 调用 物料DAL层（已改成MaterialServices）
        MaterialServices bk = new MaterialServices();

        /// <summary>
        /// 新增物料
        /// </summary>
        public bool AddMaterial(Material material)
        {
            return bk.AddMaterial(material);
        }

        /// <summary>
        /// 修改物料
        /// </summary>
        public bool UpdateMaterial(Material material)
        {
            return bk.UpdateMaterial(material);
        }

        /// <summary>
        /// 删除物料
        /// </summary>
        public bool DeleteMaterial(Material material)
        {
            return bk.DeleteMaterial(material);
        }

        /// <summary>
        /// 入库/出库 修改库存
        /// </summary>
        public bool UpdateStock(Material material)
        {
            return bk.MaterialStockInOut(material);
        }

        /// <summary>
        /// 查询所有物料
        /// </summary>
        public DataTable SelectAll()
        {
            return bk.GetAllMaterial();
        }

        /// <summary>
        /// 根据物料编号精确查询
        /// </summary>
        public DataTable SelectMaterialByCode(string str)
        {
            return bk.GetMaterialByCode(str);
        }

        /// <summary>
        /// 根据分类查询
        /// </summary>
        public DataTable SelectMaterialByCategory(string str)
        {
            return bk.GetMaterialByCategory(str);
        }

        /// <summary>
        /// 根据物料名称模糊查询
        /// </summary>
        public DataTable SelectMaterialByName(string str)
        {
            return bk.GetMaterialByName(str);
        }
    }
}