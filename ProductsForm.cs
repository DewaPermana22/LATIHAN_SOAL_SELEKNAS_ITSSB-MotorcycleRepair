using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryOut_Dekstop1
{
    public partial class ProductsForm : Form
    {
        MotorcycleRepairEntities db = new MotorcycleRepairEntities();
        public ProductsForm()
        {
            InitializeComponent();
        }

        private void generate_code()
        {
            var existing = db.Products.Count() + 1;
            var for_existing = existing.ToString("D3");
            var us_code = $"PR{for_existing}";
            textBox1.Text = us_code;
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            dgv_products.DataSource = db.Products.ToList();
            generate_code();
            textBox3.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Products products = new Products()
            {
                ProductCode = textBox1.Text,
                ProductName = textBox2.Text,
                Price = Convert.ToInt32(textBox3.Text),
            };
            db.Products.Add(products);
            db.SaveChanges();
            MessageBox.Show("Succes Creately data!");
            OnLoad(null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Products products)
            {
                input_binding.DataSource = products;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OnLoad(null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (input_binding.Current is Products product)
                {
                    db.Products.Remove(product);
                    DialogResult res = MessageBox.Show("Are you sure delete data " + product.ProductName + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        db.SaveChanges();
                        MessageBox.Show("Successfully Delete the data!");
                        OnLoad(null);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not found!");
                }
            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("All input must be filled before updating data!");
                }
                else
                {
                    db.SaveChanges();
                    MessageBox.Show("Succes Updating data!");
                    OnLoad(null);
                }
            }
            catch (Exception)
            { }
        }
    }
}
