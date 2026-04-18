using BSM.BLL;
using BSM.Models;
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
    public partial class frm : Form
    {
        public string SendAccount;
        UserManage ma = new UserManage();
        public frm(string account)
        {
            SendAccount = account;
            InitializeComponent();
        }

        private void frm_Load(object sender, EventArgs e)
        {
            textBox1.Text = SendAccount;
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("请输入完整");
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    string no = SendAccount;
                    string pwd = textBox2.Text;
                    User u = new User
                    {
                        account = no,
                        password = pwd
                    };
                    bool result = ma.UpdatePwd(u);
                    if (result)
                    {
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                }
                else
                {
                    MessageBox.Show("两次密码不一致");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}