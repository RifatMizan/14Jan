using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabTest03App.BLL;
using LabTest03App.DAL;

namespace LabTest03App
{
    public partial class ProductStoreDisplay : Form
    {
        private List<Product> productList = new List<Product>();

        public ProductStoreDisplay()
        {
            InitializeComponent();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            Product aProduct = new Product();
            aProduct.Code = codeTextBox.Text;
            aProduct.Description = descriptionTextBox.Text;
            aProduct.Quantity = Convert.ToDouble(quantityTextBox.Text);
            ProductManager aManager = new ProductManager();
            string msg = aManager.Save(aProduct);
            MessageBox.Show(msg);
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            Product products = new Product();
            ProductManager productManager = new ProductManager();
            productList = productManager.ShowAll();
            searchListView.Items.Clear();
            if (productList == null)
            {
                MessageBox.Show("List is Empty!!");
            }
            else
            {
                foreach (Product aProduct in productList)
                {
                    ListViewItem aniItem = new ListViewItem();
                    aniItem.Text = aProduct.Code;
                    aniItem.SubItems.Add(aProduct.Description);
                    aniItem.SubItems.Add(Convert.ToString(aProduct.Quantity));

                 
                    searchListView.Items.Add(aniItem);


                }
            }
            int TotalQuantity = productManager.GetTotalQuantity();
            totalQuantityTextBox.Text = TotalQuantity.ToString();

        }
    }
}
    