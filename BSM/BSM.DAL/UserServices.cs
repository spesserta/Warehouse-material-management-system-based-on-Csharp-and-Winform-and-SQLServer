using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BSM.Models;
using System.Threading.Tasks;

namespace BSM.DAL
{
    public class UserServices
    {
        /// <summary>
        /// 注册（新增操作员/管理员）
        /// </summary>
        public bool Register(User user)
        {
            // 新表：UserInfo，字段：UserName, Password, RealName, Role
            string sqlStr = @"insert into UserInfo(UserName, Password, RealName, Role) 
                              values(@UserName, @Password, @RealName, @Role)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", user.account),    // 账号 → UserName
                new SqlParameter("@Password", user.password),   // 密码
                new SqlParameter("@RealName", user.name),       // 昵称 → 真实姓名
                new SqlParameter("@Role", "操作员")              // 默认权限：操作员
            };

            return DBHelper.ExcuteCommand(sqlStr, param);
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        public bool Login(User user)
        {
            string sqlstr = @"select UserName from UserInfo 
                              where UserName=@UserName and Password=@Password";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", user.account),
                new SqlParameter("@Password", user.password)
            };

            DataTable dt = DBHelper.GetDataTable(sqlstr, param);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public bool UpdatePwd(User user)
        {
            string sqlStr = @"update UserInfo set Password=@Password where UserName = @UserName";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", user.account),
                new SqlParameter("@Password", user.password)
            };

            return DBHelper.ExcuteCommand(sqlStr, param);
        }
    }
}