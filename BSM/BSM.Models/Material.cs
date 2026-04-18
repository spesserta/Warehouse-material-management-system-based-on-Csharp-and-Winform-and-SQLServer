using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.Models
{
    public class Material
    {
        // 物料编号（主键，对应SQL：MaterialID）
        public string materialId { get; set; }

        // 物料名称
        public string materialName { get; set; }

        // 规格型号
        public string spec { get; set; }

        // 分类ID（对应物料分类表）
        public int categoryId { get; set; }

        // 当前库存数量
        public int stock { get; set; }

        // 单位：个/米/套/根
        public string unit { get; set; }

        // 供应商
        public string supplier { get; set; }

        // 库存预警值
        public int warningStock { get; set; }

        // 备注
        public string remark { get; set; }
    }
}