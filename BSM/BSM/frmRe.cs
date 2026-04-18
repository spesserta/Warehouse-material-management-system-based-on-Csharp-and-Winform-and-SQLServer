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
    public partial class frmRe : Form
    {
        public frmRe()
        {
            InitializeComponent();
        }

        MaterialManage bk = new MaterialManage();
        Verification ver = new Verification();
        byte[] bytes;
        int currentStock; // 当前库存（替代原num）
        string currentMaterialId; // 当前物料编号

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void getAll()
        {
            // 替换旧Select()为新SelectAll()
            dataGridView1.DataSource = bk.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text.Equals(""))
                {
                    MessageBox.Show("请输入物料编号");
                    return;
                }
                label18.Text = "";
                // 替换SelectBookOne为SelectMaterialByCode
                DataTable dt = bk.SelectMaterialByCode(textBox3.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到该编号物料！！！");
                    return;
                }
                dataGridView1.DataSource = dt;
                MyBook(textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("未找到该编号物料！！！\n" + ex.Message);
            }
        }

        public void MyBook(string materialId)
        {
            DataTable dt = bk.SelectMaterialByCode(materialId);
            if (dt.Rows.Count == 0) return;

            DataRow row = dt.Rows[0];
            textBox1.Text = materialId;
            currentMaterialId = materialId;

            // 字段全替换：图书→物料
            label19.Text = row["MaterialName"].ToString();      // 书名→物料名称
            label8.Text = row["Spec"].ToString();               // 主编→规格型号
            label9.Text = row["Supplier"].ToString();           // 出版社→供应商
            label11.Text = GetCategoryName(Convert.ToInt32(row["CategoryID"])); // 分类→分类名
            currentStock = Convert.ToInt32(row["Stock"]);       // 数量→库存
            label13.Text = currentStock.ToString();
            label15.Text = row["Remark"].ToString();            // 来源→备注

            // 图片字段（如需保留可自行添加，这里先置空）
            pictureBox1.Image = null;
            label18.Text = "该物料暂无图片";

            label6.Text = currentStock.ToString();
        }

        // 分类ID转分类名（和FrmAdd保持一致）
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("出库失败！！！请先查询物料");
                return;
            }

            if (currentStock <= 0)
            {
                MessageBox.Show("对不起！库存不足");
                return;
            }

            // 出库：库存-1
            int newStock = currentStock - 1;
            Material material = new Material
            {
                materialId = currentMaterialId,
                stock = newStock
            };

            bool result = bk.UpdateStock(material);
            if (result)
            {
                MessageBox.Show("出库成功！！！");
                currentStock = newStock;
                label6.Text = currentStock.ToString();
                label13.Text = currentStock.ToString();
                getAll();
            }
            else
            {
                MessageBox.Show("出库失败");
            }
        }

        private void frmRe_Load(object sender, EventArgs e)
        {
            getAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("编号为空！！！");
                return;
            }

            // 验证编号格式（字母+数字，如M001）
            if (!ver.IsMaterialCode(textBox2.Text))
            {
                MessageBox.Show("编号格式有误！！！");
                return;
            }

            try
            {
                DataTable dt = bk.SelectMaterialByCode(textBox2.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("没有该物料！！！");
                    return;
                }

                int stock = Convert.ToInt32(dt.Rows[0]["Stock"]);
                string materialId = textBox2.Text;

                // 入库：库存+1
                int newStock = stock + 1;
                Material material = new Material
                {
                    materialId = materialId,
                    stock = newStock
                };

                bool result = bk.UpdateStock(material);
                if (result)
                {
                    textBox3.Text = textBox2.Text;
                    MyBook(textBox2.Text);
                    MessageBox.Show("入库成功！！！");
                    getAll();
                }
                else
                {
                    MessageBox.Show("入库失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("入库失败！！！\n" + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getAll();
            textBox3.Text = "";
            textBox1.Text = "";
            label19.Text = "";
            label8.Text = "";
            label9.Text = "";
            label11.Text = "";
            label13.Text = "";
            label15.Text = "";
            label18.Text = "";
            pictureBox1.Image = null;
            currentMaterialId = "";
            currentStock = 0;
        }
    }
}