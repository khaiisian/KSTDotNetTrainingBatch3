using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSTDotNetTrainingBatch3.WindowFormsApp
{
    public partial class FrmProduct : Form
    {
        private readonly AppDbContext _db = new AppDbContext();
        private int editId = 0;

        public FrmProduct()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (editId == 0)
            {
                Save();
            } else
            {
                Edit();
            }

            BindData();
            ClearData();
        }

        private void Save()
        {
            var productName = txtName.Text.Trim();
            int quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

            var product = new TblProduct()
            {
                ProductName = productName,
                Quantity = quantity,
                Price = price,
                CreatedDateTime = DateTime.Now,
                DeleteFlag = false
            };

            _db.TblProducts.Add(product);
            int result = _db.SaveChanges();
            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            MessageBox.Show(message);
        }

        private void Edit()
        {
            var productName = txtName.Text.Trim();
            int quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());

            var product = _db.TblProducts
                .Where(x=>x.DeleteFlag ==  false)
                .FirstOrDefault(x => x.ProductId == editId);

            if (product is null)
            {
                MessageBox.Show("No data found");
                BindData();
                return;
            }

            product.ProductName = productName;
            product.Quantity = quantity;
            product.Price = price;
            product.ModifiedDateTime = DateTime.Now;

            int result = _db.SaveChanges();
            string message = result > 0 ? "Update successful" : "Update Failed";
            MessageBox.Show(message);
        }

        private void ClearData()
        {
            txtName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtName.Focus();
        }

        private void BindData()
        {
            dgvData.DataSource = _db.TblProducts
                .Where(x=>x.DeleteFlag == false)
                .OrderByDescending(x => x.ProductId)
                .ToList();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            // Edit
            if (e.ColumnIndex == 0)
            {
                var value = dgvData.Rows[e.RowIndex].Cells["colProductId"].Value.ToString();
                int id = Convert.ToInt32(value);

                var product = _db.TblProducts
                    .Where(x=>x.DeleteFlag==false)
                    .FirstOrDefault(x=> x.ProductId == id);

                if(product is null)
                {
                    MessageBox.Show("No data found");
                    BindData();
                    return;
                }

                txtName.Text = product.ProductName;
                txtQuantity.Text = product.Quantity.ToString();
                txtPrice.Text = product.Price.ToString();
                editId = product.ProductId;
            }
            else if (e.ColumnIndex == 1)
            {
                var res = MessageBox.Show("Are you sure to delete the record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No) return;

                var value = dgvData.Rows[e.RowIndex].Cells["colProductId"].Value.ToString();
                int id = Convert.ToInt32(value);

                var product = _db.TblProducts
                    .Where(x => x.DeleteFlag == false)
                    .FirstOrDefault(x => x.ProductId == id);

                if (product is null)
                {
                    MessageBox.Show("No data found");
                    BindData();
                    return;
                }

                product.DeleteFlag = true;
                int result = _db.SaveChanges();
                string message = result > 0 ? "Delete successful" : "Delete Failed";
                MessageBox.Show(message);

                BindData();
                return;
            }
        }
    }
}
