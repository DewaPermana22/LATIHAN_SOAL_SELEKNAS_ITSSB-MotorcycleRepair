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
    public partial class Transaction : Form
    {
        Users user;
        List<string> serviceID = new List<string>();
        List<string> productID = new List<string>();
        List<string> list_service_name = new List<string>();
        List<string> list_product_code = new List<string>();
        List<int> list_price_product = new List<int>();
        List<int> list_service_price = new List<int>();
        int amount_services = 0;
        int amount_products = 0;
        int price_product = 0;
        int price_service = 0;
        int total_price = 0;
        int change = 0;
        MotorcycleRepairEntities db = new MotorcycleRepairEntities();
        public Transaction(Users users)
        {
            this.user = users;
            InitializeComponent();
        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Today.ToString("dd/MMM/yy");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cmb_mechaninc.DataSource = db.Mechanics.ToList();
            GenerateTransactionNumber();
        }

        private void GenerateTransactionNumber()
        {
            var year = DateTime.Now.ToString("MMyyyy");
            var current_transaction = db.TransactionService.Where( ts => ts.TransactionDate.ToString() == year).ToList();
            int queque = current_transaction.Count + 1;
            String sequence_number = queque.ToString("D3");
            textBox1.Text = $"T{sequence_number}{year}";

        }
        void count_total_services()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    amount_services++;
                }
            }
        }
        void count_total_products()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow)
                {
                    amount_products++;
                }
            }
        }

        private void total_price_product()
        {
            price_product = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow && row.Cells["prd_price"] != null)
                {
                    int cost = Convert.ToInt32(row.Cells["prd_price"].Value?.ToString());
                    price_product += cost;
                }
            }
        }
        private void total_price_service()
        {
            price_service = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow && row.Cells["services_cost"] != null)
                {
                    int cost = Convert.ToInt32(row.Cells["services_cost"].Value?.ToString());
                    price_service += cost;
                }
            }
        }

        private void total_all(int pay)
        {
            total_price = price_service + price_product;
            change = pay - total_price;
            textBox9.Text = total_price.ToString();
            textBox11.Text = change.ToString();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "services_code")
                {
                    String input = dataGridView1.Rows[e.RowIndex].Cells["services_code"].Value?.ToString();
                    if (!String.IsNullOrEmpty(input))
                    {
                        try
                        {
                            var name_cost = db.MotorcycleServices.Where(f => f.ServiceCode == input)
                                               .Select(f => new
                                               {
                                                   f.ServiceCode,
                                                   f.ServiceName,
                                                   f.Cost,
                                               }).FirstOrDefault();
                            dataGridView1.Rows[e.RowIndex].Cells["services_name"].Value = name_cost.ServiceName;
                            dataGridView1.Rows[e.RowIndex].Cells["services_cost"].Value = name_cost.Cost;
                            serviceID.Add(name_cost.ServiceCode);
                            list_service_name.Add(name_cost.ServiceName);
                            list_service_price.Add((int)name_cost.Cost);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Services Not Found!");
                    }
                    total_price_service();
                    total_all(0);
                    amount_services = 0;
                    count_total_services();
                    textBox5.Text = amount_services.ToString();
                    textBox6.Text = price_service.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView2.Columns[e.ColumnIndex].Name == "prd_code")
                {
                    try
                    {
                        String input = dataGridView2.Rows[e.RowIndex].Cells["prd_code"].Value?.ToString();
                        Console.WriteLine(input);
                        if (!String.IsNullOrEmpty(input))
                        {
                            var name_cost = db.Products.Where(f => f.ProductCode == input)
                                .Select(f => new
                                {
                                    f.ProductCode,
                                    f.ProductName,
                                    f.Price,
                                }).FirstOrDefault();
                            dataGridView2.Rows[e.RowIndex].Cells["prd_name"].Value = name_cost.ProductName;
                            dataGridView2.Rows[e.RowIndex].Cells["prd_price"].Value = name_cost.Price;
                            productID.Add(name_cost.ProductCode);
                            list_price_product.Add((int)name_cost.Price);
                        }
                        else
                        {
                            MessageBox.Show("Products Not Found!");
                        }
                        total_price_product();
                        total_all(0);
                        amount_products = 0;
                        count_total_products();
                        textBox7.Text = amount_products.ToString();
                        textBox8.Text = price_product.ToString();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            var pay = Convert.ToInt32(textBox10.Text);
            total_all(pay);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var transactionService = new TransactionService
                    {
                        TransactionNumber = textBox1.Text,
                        TransactionDate = DateTime.Now,
                        TotalProductPrice = Convert.ToInt32(textBox8.Text),
                        TotalServiceCost = Convert.ToInt32(textBox6.Text),
                        PoliceRegistrationNumber = textBox3.Text,
                        Damage = textBox4.Text,
                        TotalCharge = Convert.ToInt32(textBox9.Text),
                        Paid = Convert.ToInt32(textBox10.Text),
                        ChangeMoney = Convert.ToInt32(textBox11.Text),
                        UserCode = user.UserCode,
                        MechanicCode = (string)comboBox1.SelectedValue,
                    };
                    db.TransactionService.Add(transactionService);
                    for (int i = 0; i < serviceID.Count; i++)
                    {
                        var serviceCode = serviceID[i];
                        var servicePrice = list_service_price[i];

                        var detailService = new DetailService
                        {
                            TransactionNumber = transactionService.TransactionNumber,
                            ServiceCode = serviceCode,
                            Cost = servicePrice,
                        };
                        db.DetailService.Add(detailService);

                        var existingService = db.MotorcycleServices.FirstOrDefault(s => s.ServiceCode == serviceCode);
                        if (existingService == null)
                        {
                            var newService = new MotorcycleServices
                            {
                                ServiceCode = serviceCode,
                                ServiceName = list_service_name[i],
                                Cost = servicePrice,
                            };
                            db.MotorcycleServices.Add(newService);
                        }
                    }
                    for (int i = 0; i < productID.Count; i++)
                    {
                        var detailProduct = new DetailProduct
                        {
                            ProductCode = productID[i],
                            Price = list_price_product[i],
                            Amount = Convert.ToInt32(textBox7.Text),
                            Total = transactionService.TotalProductPrice,
                        };
                        db.DetailProduct.Add(detailProduct);
                    }
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    MessageBox.Show("Transaction saved successfully!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}
