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
    public partial class UserForm : Form
    {
        MotorcycleRepairEntities db = new MotorcycleRepairEntities();
        public UserForm()
        {
            InitializeComponent();
        }

        private void generate_code()
        {
            var now = DateTime.Now.Year % 100;
            var existing = db.Users.Count() + 1;
            var for_existing = existing.ToString("D2");
            var us_code = $"USR-{now}-{for_existing}";
            textBox1.Text = us_code;
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            generate_code();
            dgv_users.DataSource = db.Users.ToList();
            textBox3.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users user = new Users()
            {
                UserCode = textBox1.Text,
                UserName = textBox2.Text,
                UserPassword = textBox3.Text,
            };
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            MessageBox.Show("Succes Creately data!");
            OnLoad(null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Users user)
            {
                input_binding.DataSource = user;
            }
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
            {}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (input_binding.Current is Users user)
                {
                    db.Users.Remove(user);
                    DialogResult res = MessageBox.Show("Are you sure delete data " + user.UserName + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void button4_Click(object sender, EventArgs e)
        {
            OnLoad(null);
        }
    }
}
    