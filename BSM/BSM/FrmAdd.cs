using BSM.Models;
using BSM.BLL;
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
using System.Data.SqlClient;

namespace BSM
{
    public partial class FrmAdd : Form
    {
        public FrmAdd()
        {
            InitializeComponent();
        }
        byte[] bytes;
        public string materialId;
        MaterialManage bk = new MaterialManage();
        Verification ver = new Verification();

        private void FrmAdd_Load(object sender, EventArgs e)
        {
            getAll();
        }

        private void getAll()
        {
            // 替换旧的 Select() 为新的 SelectAll()，适配物料管理
            dataGridView1.DataSource = bk.SelectAll();
        }

        public bool checkNull()
        {
            bool a = true;
            if (txtMaterialName.Text.Trim().Equals("") &&
               txtSpec.Text.Trim().Equals("") &&
               txtSupplier.Text.Trim().Equals("") &&
               cbCategory.Text.Trim().Equals("") &&
               txtStock.Text.Trim().Equals("") &&
               txtMaterialId.Text.Trim().Equals(""))
            {
                a = false;
            }
            return a;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (checkNull())
            {
                string name = txtMaterialName.Text;
                string spec = txtSpec.Text;
                string supplier = txtSupplier.Text;
                string category = cbCategory.Text;
                string stock = txtStock.Text;
                string remark = txtRemark.Text;
                string materialId = txtMaterialId.Text;
                string unit = cbUnit.Text;
                string warningStock = txtWarningStock.Text;

                if (!ver.IsNumber(txtStock.Text) || !ver.IsNumber(txtWarningStock.Text))
                {
                    MessageBox.Show("库存和预警库存只能为数字！！！");
                    return;
                }

                // 替换 Book 为 Material，适配新实体
                Material material = new Material
                {
                    materialId = materialId,
                    materialName = name,
                    spec = spec,
                    supplier = supplier,
                    categoryId = GetCategoryId(category), // 分类名转ID
                    stock = int.Parse(stock),
                    unit = unit,
                    warningStock = string.IsNullOrEmpty(warningStock) ? 5 : int.Parse(warningStock),
                    remark = remark
                    // 图片字段已从Material实体移除，如需保留可自行添加
                };

                // 替换 Addbook 为 AddMaterial
                bool result = bk.AddMaterial(material);
                if (result)
                {
                    MessageBox.Show("添加成功");
                    getAll();
                    ClearAll();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                MessageBox.Show("请输入完整信息");
            }
        }

        // 分类名转分类ID（适配MaterialCategory表）
        private int GetCategoryId(string categoryName)
        {
            switch (categoryName)
            {
                case "电子元器件": return 1;
                case "结构件": return 2;
                case "标准件": return 3;
                case "耗材": return 4;
                default: return 0;
            }
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "请选择物料图片";
            openfile.Filter = "图片(*.jpg;*.bmp;*png)|*.jpeg;*.jpg;*.bmp;*.png|AllFiles(*.*)|*.*";
            if (DialogResult.OK == openfile.ShowDialog())
            {
                try
                {
                    bytes = File.ReadAllBytes(openfile.FileName);
                    pictureBox1.Image = System.Drawing.Image.FromStream(new MemoryStream(bytes));
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch { }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选中一行进行操作");
                return;
            }

            // 适配新物料表字段顺序
            materialId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtMaterialId.Text = materialId;
            txtMaterialName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtSpec.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cbCategory.Text = GetCategoryName(int.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()));
            txtStock.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            cbUnit.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtSupplier.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            txtWarningStock.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            txtRemark.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();

            // 图片字段如需保留，可自行添加
            pictureBox1.Image = null;
        }

        // 分类ID转分类名
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            txtMaterialId.Text = "";
            txtMaterialName.Text = "";
            txtSpec.Text = "";
            txtSupplier.Text = "";
            cbCategory.Text = "";
            txtStock.Text = "";
            cbUnit.Text = "";
            txtWarningStock.Text = "";
            txtRemark.Text = "";
            pictureBox1.Image = null;
            bytes = null;
        }
    }
}