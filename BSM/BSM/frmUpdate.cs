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
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        // 彻底砍掉bytes字段（图片相关全删）
        public string materialId;
        MaterialManage bk = new MaterialManage();

        private void getAll()
        {
            dataGridView1.DataSource = bk.SelectAll();
        }

        public bool checkNull()
        {
            // 只校验核心字段，删掉category、remark等
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtPress.Text) ||
                string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                return false;
            }
            return true;
        }

        // 彻底砍掉图片按钮方法（直接留空或删除）
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本版本已简化，不支持图片上传");
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {
            if (!checkNull())
            {
                MessageBox.Show("请输入完整信息");
                return;
            }

            try
            {
                // 🌟 核心修复：只给Material模型里存在的字段赋值！
                Material material = new Material
                {
                    materialId = materialId,
                    materialName = txtName.Text.Trim(),
                    spec = txtAuthor.Text.Trim(), // 保留规格（如果不需要可删）
                    supplier = txtPress.Text.Trim(),
                    stock = int.Parse(txtNumber.Text.Trim()),
                    remark = txtFrom.Text.Trim(),
                    categoryId = cbClass.SelectedIndex,
                    // 彻底删掉category、materialPhoto、remark这些不存在的字段！
                };

                bool result = bk.UpdateMaterial(material);
                if (result)
                {
                    MessageBox.Show("修改成功");
                    getAll();
                    ClearAll();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选中一行进行操作");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            materialId = row.Cells[0].Value?.ToString() ?? "";
            txtName.Text = row.Cells[1].Value?.ToString() ?? "";
            txtAuthor.Text = row.Cells[2].Value?.ToString() ?? "";
            txtPress.Text = row.Cells[3].Value?.ToString() ?? "";
            txtNumber.Text = row.Cells[4].Value?.ToString() ?? "";

            // 彻底砍掉图片相关的赋值逻辑
            pictureBox1.Image = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            txtName.Text = "";
            txtAuthor.Text = "";
            txtPress.Text = "";
            txtNumber.Text = "";
            pictureBox1.Image = null;
            materialId = "";
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            getAll();
        }
    }
}