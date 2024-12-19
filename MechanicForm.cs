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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TryOut_Dekstop1
{
    public partial class MechanicForm : Form
    {
        MotorcycleRepairEntities db = new MotorcycleRepairEntities();
        public MechanicForm()
        {
            InitializeComponent();
        }

        private void generate_code()
        {
            var existing = db.Mechanics.Count() + 1;
            var for_existing = existing.ToString("D3");
            var us_code = $"MC{for_existing}";
            textBox1.Text = us_code;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Mechanics mechanics)
            {
                input_mechaninc.DataSource = mechanics;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void MechanicForm_Load(object sender, EventArgs e)
        {
            generate_code();
            textBox2.Text = string.Empty;
            dgv_mechaninc.DataSource = db.Mechanics.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mechanics mechanics = new Mechanics()
            {
                MechanicCode = textBox1.Text,
                MechanicName = textBox2.Text,
            };
            db.Mechanics.AddOrUpdate(mechanics);
            db.SaveChanges();
            OnLoad(null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBox2.Text))
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (input_mechaninc.Current is Mechanics mechanics)
                {
                    db.Mechanics.Remove(mechanics);
                    DialogResult res = MessageBox.Show("Are you sure delete data " + mechanics.MechanicName + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
