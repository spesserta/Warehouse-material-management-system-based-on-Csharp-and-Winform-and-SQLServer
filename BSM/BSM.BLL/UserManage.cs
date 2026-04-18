using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSM.DAL;
using BSM.Models;
using System.Threading.Tasks;

namespace BSM.BLL
{
    public class UserManage
    {
        // 已经是改造后的 UserServices，无需改动
        UserServices service = new UserServices();

        // 注册（已适配新数据库 UserInfo 表）
        public bool Register(User user)
        {
            return service.Register(user);
        }

        // 登录验证
        public bool Login(User user)
        {
            return service.Login(user);
        }

        // 修改密码
        public bool UpdatePwd(User user)
        {
            return service.UpdatePwd(user);
        }
    }
}