using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TryOut_Dekstop1
{
    public partial class Form1 : Form
    {
        MotorcycleRepairEntities db = new MotorcycleRepairEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "USR-22-01";
            textBox2.Text = "iXEyVoLc";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = db.Users.FirstOrDefault( u => u.UserCode == textBox1.Text && u.UserPassword == textBox2.Text );
            if ( login != null )
            {
               MainForm main = new MainForm(login);
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show("User Not Found!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
