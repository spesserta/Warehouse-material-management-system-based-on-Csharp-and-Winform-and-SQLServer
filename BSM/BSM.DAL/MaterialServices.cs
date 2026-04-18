using BSM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.DAL
{
    public class MaterialServices  // 把 BookServices → MaterialServices
    {
        /// <summary>
        /// 新增物料（原：新增图书）
        /// </summary>
        public bool AddMaterial(Material material)  // Addbook → AddMaterial
        {
            // SQL 完全匹配你的 Material 表
            string sqlStr = @"insert into Material 
                             (MaterialID, MaterialName, Spec, CategoryID, Stock, Unit, Supplier, WarningStock, Remark) 
                             values(@MaterialID, @MaterialName, @Spec, @CategoryID, @Stock, @Unit, @Supplier, @WarningStock, @Remark)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MaterialID", material.materialId),
                new SqlParameter("@MaterialName", material.materialName),
                new SqlParameter("@Spec", material.spec),
                new SqlParameter("@CategoryID", material.categoryId),
                new SqlParameter("@Stock", material.stock),
                new SqlParameter("@Unit", material.unit),
                new SqlParameter("@Supplier", material.supplier),
                new SqlParameter("@WarningStock", material.warningStock),
                new SqlParameter("@Remark", material.remark)
            };
            return DBHelper.ExcuteCommand(sqlStr, param);
        }

        /// <summary>
        /// 修改物料信息
        /// </summary>
        public bool UpdateMaterial(Material material) // Updatebook → UpdateMaterial
        {
            string sqlStr = @"update Material set 
                             MaterialName=@MaterialName, 
                             Spec=@Spec, 
                             CategoryID=@CategoryID, 
                             Stock=@Stock, 
                             Unit=@Unit, 
                             Supplier=@Supplier, 
                             WarningStock=@WarningStock, 
                             Remark=@Remark 
                             where MaterialID=@MaterialID";

            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@MaterialID", material.materialId),
                 new SqlParameter("@MaterialName", material.materialName),
                 new SqlParameter("@Spec", material.spec),
                 new SqlParameter("@CategoryID", material.categoryId),
                 new SqlParameter("@Stock", material.stock),
                 new SqlParameter("@Unit", material.unit),
                 new SqlParameter("@Supplier", material.supplier),
                 new SqlParameter("@WarningStock", material.warningStock),
                 new SqlParameter("@Remark", material.remark)
            };
            return DBHelper.ExcuteCommand(sqlStr, param);
        }

        /// <summary>
        /// 入库 / 出库 修改库存（原：图书借阅归还）
        /// </summary>
        public bool MaterialStockInOut(Material material)
        {
            string sqlStr = "update Material set Stock=@Stock where MaterialID=@MaterialID";

            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@MaterialID", material.materialId),
                 new SqlParameter("@Stock", material.stock),
            };
            return DBHelper.ExcuteCommand(sqlStr, param);
        }

        /// <summary>
        /// 删除物料
        /// </summary>
        public bool DeleteMaterial(Material material)
        {
            string str = "delete From Material where MaterialID = @MaterialID";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@MaterialID", material.materialId)
            };

            return DBHelper.ExcuteCommand(str, param);
        }

        /// <summary>
        /// 根据 物料编号 精确查找
        /// </summary>
        public DataTable GetMaterialByCode(string a)
        {
            string strsql = string.Format("select * from Material where MaterialID='{0}'", a);
            SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt.Tables[0];
        }

        /// <summary>
        /// 根据 分类 模糊查找
        /// </summary>
        public DataTable GetMaterialByCategory(string a)
        {
            string strsql = string.Format("select * from Material where CategoryID like N'%{0}%'", a);
            SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt.Tables[0];
        }

        /// <summary>
        /// 根据 物料名称 模糊查找
        /// </summary>
        public DataTable GetMaterialByName(string a)
        {
            string strsql = string.Format("select * from Material where MaterialName like N'%{0}%'", a);
            SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt.Tables[0];
        }

        /// <summary>
        /// 查询所有物料
        /// </summary>
        public DataTable GetAllMaterial()
        {
            string strsql = "select * from Material";
            SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt.Tables[0];
        }

        // 无用旧方法已清理，保持代码干净
    }
}