using BSM.BLL;
using BSM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSM
{
    public partial class frmfel : Form
    {
        public frmfel()
        {
            InitializeComponent();
        }

        byte[] bytes;
        MaterialManage bk = new MaterialManage();

        // 精确查找：按物料编号
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text.Equals(""))
                {
                    MessageBox.Show("请输入物料编号");
                    return;
                }

                DataTable dt = bk.SelectMaterialByCode(textBox3.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到该编号物料！！！");
                    return;
                }

                DataRow row = dt.Rows[0];
                label5.Text = row["MaterialName"].ToString();      // 书名→物料名称
                label7.Text = row["Spec"].ToString();               // 主编→规格型号
                label9.Text = row["Supplier"].ToString();           // 出版社→供应商
                label11.Text = GetCategoryName(Convert.ToInt32(row["CategoryID"])); // 分类→分类名
                label13.Text = row["Stock"].ToString();              // 数量→库存
                label15.Text = row["Remark"].ToString();             // 来源→备注

                // 图片字段（如需保留可自行添加，这里先置空）
                pictureBox1.Image = null;
                label18.Text = "该物料暂无图片";
            }
            catch (Exception ex)
            {
                MessageBox.Show("未找到该编号物料！！！\n" + ex.Message);
            }
        }

        // 分类ID转分类名（和其他界面保持一致）
        private string GetCategoryName(int categoryId)
        {
            switch (categoryId)
            {
                case 1: return "电子元器件";
                case 2: return "结构件";
                case 3: return "标准件";
                case 4: return "耗材";
                default: return "";
            }
        }

        // 加载全部物料
        private void getAll()
        {
            dataGridView1.DataSource = bk.SelectAll();
        }

        private void frmfel_Load(object sender, EventArgs e)
        {
            getAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // 模糊查找：按物料名称
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("请输入物料名称");
                return;
            }
            dataGridView1.DataSource = bk.SelectMaterialByName(textBox1.Text);
        }

        // 按类查找：按物料分类
        private void button2_Click(object sender, EventArgs e)
        {
            if (cbClass.Text.Equals(""))
            {
                MessageBox.Show("请选择类别");
                return;
            }
            dataGridView1.DataSource = bk.SelectMaterialByCategory(cbClass.Text);
        }

        // 查找全部
        private void button6_Click(object sender, EventArgs e)
        {
            getAll();
        }

        // 删除物料
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("请输入物料编号");
                return;
            }

            // 二次确认，防止误删
            if (MessageBox.Show("确定要删除该物料吗？此操作不可逆！", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            try
            {
                string materialId = textBox2.Text;
                Material material = new Material { materialId = materialId };
                bool result = bk.DeleteMaterial(material);

                if (result)
                {
                    MessageBox.Show("删除成功");
                    getAll();
                    textBox2.Text = "";
                    // 清空详情
                    label5.Text = "";
                    label7.Text = "";
                    label9.Text = "";
                    label11.Text = "";
                    label13.Text = "";
                    label15.Text = "";
                    pictureBox1.Image = null;
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败！\n" + ex.Message);
            }
        }
    }
}