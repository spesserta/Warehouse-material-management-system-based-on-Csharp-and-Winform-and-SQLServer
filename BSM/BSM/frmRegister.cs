using BSM.BLL;
using BSM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSM
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        Verification ver = new Verification();
        UserManage ma = new UserManage();

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string count = txt_count.Text.Trim();
            string pwd = txt_pwd.Text.Trim();
            string nc = txt_name.Text.Trim();
            string rePwd = txt_submit.Text.Trim();

            // 1. 非空验证
            if (count == "" || pwd == "" || nc == "" || rePwd == "")
            {
                MessageBox.Show("请输入完整信息！！！");
                return;
            }

            // 2. 账号验证：4位数字
            if (!ver.IsCode(count))
            {
                MessageBox.Show("请输入4位数字账号！！！");
                return;
            }

            // 3. 昵称验证：中文
            if (!ver.IsChinese(nc))
            {
                MessageBox.Show("请输入中文昵称！！！");
                return;
            }

            // 4. 密码一致性验证
            if (pwd != rePwd)
            {
                MessageBox.Show("两次输入不一致！！！");
                return;
            }

            // 5. 注册：自动MD5加密密码
            User user = new User
            {
                account = count,
                password = ToMD5(pwd), // 加密后存入数据库
                name = nc
                // Role 默认为操作员，无需手动赋值
            };

            bool result = ma.Register(user);

            if (result)
            {
                MessageBox.Show("注册成功！！！");
                // 注册成功后清空文本框
                txt_count.Text = "";
                txt_name.Text = "";
                txt_pwd.Text = "";
                txt_submit.Text = "";
            }
            else
            {
                MessageBox.Show("账号已存在！！！");
            }
        }

        /// <summary>
        /// MD5加密工具方法（和登录界面保持一致）
        /// </summary>
        public static string ToMD5(string source)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(source);
            data = md5.ComputeHash(data);
            foreach (var item in data)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            txt_pwd.PasswordChar = '*';
            txt_submit.PasswordChar = '*';
        }
    }
}