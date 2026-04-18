using BSM.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSM
{
    public partial class frmMain : Form
    {
        public string SendAccount;

        // 接收登录传过来的账号和密码（密码目前主界面未使用）
        public frmMain(string account, string pwd)
        {
            SendAccount = account;
            InitializeComponent();
        }

        MaterialManage bk = new MaterialManage(); // 物料业务层

        private void frmMain_Load(object sender, EventArgs e)
        {
            label1.Text = System.DateTime.Now.ToLongDateString();

            // 🌟 修复核心BUG：原写法查的是Book表，现在直接显示当前登录用户名
            // 从UserInfo表中获取真实姓名（如果没有则显示账号）
            try
            {
                // 这里我们直接用本地获取，因为DAL层主要是针对物料表的
                label3.Text = SendAccount;
                // 如果你想从数据库查真实姓名，可以加一个UserServices.GetRealName方法
            }
            catch (Exception)
            {
                label3.Text = "未知用户";
            }
        }

        // 🌟 菜单文字替换：添加物料
        private void 添加物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAdd mainForm = new FrmAdd();
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.Show();
        }

        // 🌟 菜单文字替换：删除/查找物料
        private void Del_Click(object sender, EventArgs e)
        {
            // 注意：你需要创建 frmfel.cs（删除/查找界面），或者指向 frmUpdate
            frmfel mainForm = new frmfel();
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.Show();
        }

        // 🌟 菜单文字替换：修改密码
        private void 修改密码ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide(); // 关闭当前主界面
            frm mainForm = new frm(SendAccount);
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.ShowDialog(); // 以对话框形式打开修改密码
            this.Show(); // 关闭后返回主界面
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 🌟 菜单文字替换：修改物料
        private void 修改物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdate mainForm = new frmUpdate();
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.Show();
        }

        // 🌟 菜单文字替换：物料出入库（借还管理）
        private void 物料出入管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRe mainForm = new frmRe();
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.Show();
        }
    }
}