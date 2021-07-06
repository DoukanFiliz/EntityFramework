using EFCodeFirstDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFCodeFirstDemo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal productDal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = productDal.GetAll();
        }

        private void SearchData(string key)
        {
            //dataGridView1.DataSource = productDal.GetAll().Where(p => p.Name.ToString().ToLower().Contains(key.ToLower())).ToList();
            dataGridView1.DataSource = productDal.GetByName(key);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productDal.Add(new Product
            {
                Name = textBox1.Text,
                UnitPrice = Convert.ToDecimal(textBox2.Text),
                StockAmount = Convert.ToInt32(textBox3.Text)
            });
            LoadData();
            MessageBox.Show("Product Added!", "Succesful");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Length < 1)
            {
                MessageBox.Show("Some informations are missing!", "Warning");
            }
            else
            {
                productDal.Update(new Product
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                    Name = textBox6.Text.ToString(),
                    UnitPrice = Convert.ToDecimal(textBox5.Text),
                    StockAmount = Convert.ToInt32(textBox4.Text)
                });
                LoadData();
                MessageBox.Show("Product Updated!", "Succesful");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productDal.Remove(new Product { Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value) });
            LoadData();
            MessageBox.Show("Product Removed!", "Succesful");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(tbxSearch.Text);
        }
    }
}
