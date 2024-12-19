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
    public partial class MainForm : Form
    {
        Users user;
        public MainForm(Users user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction(user);
            transaction.ShowDialog();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm();
            productsForm.ShowDialog();
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Services services = new Services();
            services.ShowDialog();
        }

        private void mechanicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MechanicForm mechani = new MechanicForm();
            mechani.ShowDialog();
        }
    }
}
