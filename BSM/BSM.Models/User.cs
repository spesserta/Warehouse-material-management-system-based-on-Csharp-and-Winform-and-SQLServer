using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.Models
{
    public class User
    {
        // 登录账号（对应 UserName）
        public string account { get; set; }

        // 登录密码
        public string password { get; set; }

        // 真实姓名（对应 RealName）
        public string name { get; set; }

        // 角色/权限：管理员 / 操作员（上位机系统必备）
        public string role { get; set; }
    }
}