using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTest03App.DAL.DBGateway
{
    internal class ProductDBGateway
    {
        private string connectionStr =
            ConfigurationManager.ConnectionStrings["ConnectionStringOfProductDB"].ConnectionString;

        private SqlConnection aConnection;

        public ProductDBGateway()
        {
            aConnection = new SqlConnection(connectionStr);
        }

        public void Save(Product aProduct)
        {
            string query = "INSERT INTO t_Product VALUES ('" + aProduct.Code + "','" + aProduct.Description + "','" +
                           aProduct.Quantity + "')";
            aConnection.Open();
            SqlCommand aSqlCommand = new SqlCommand(query, aConnection);
            aSqlCommand.ExecuteNonQuery();
            aConnection.Close();
        }

        public int Update(Product aProduct)
        {
            string query = "UPDATE t_Product SET quantity='" + aProduct.Quantity + "' WHERE code='" + aProduct.Code +
                           "'";
            aConnection.Open();
            SqlCommand aCommand=new SqlCommand(query,aConnection);
            int rowAffected=aCommand.ExecuteNonQuery();
            aConnection.Close();
            return rowAffected;
        }

        public Product Find(string code)
        {
            string query = "SELECT * FROM t_Product WHERE Code= '" + code + "'";
            aConnection.Open();
            SqlCommand aSqlCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aSqlCommand.ExecuteReader();
            Product aProduct;
            if (aReader.HasRows)
            {
                aProduct = new Product();
                aReader.Read();
                aProduct.Id = Convert.ToInt32(aReader["Id"]);
                aProduct.Code = aReader["Code"].ToString();
                aProduct.Description = aReader["Description"].ToString();
                aProduct.Quantity = Convert.ToDouble(aReader["Quantity"]);
                aReader.Close();
                aConnection.Close();
                return aProduct;
            }
            else
            {
                aConnection.Close();
                return null;
            }
        }


        public List<Product> ShowAll()
        {
            
            string query = "SELECT * FROM t_Product";
            aConnection.Open();
            SqlCommand aSqlCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aSqlCommand.ExecuteReader();
            List<Product> productsList = new List<Product>();
            while (aReader.Read())
            {
                Product aProduct=new Product();
                aProduct = new Product();
                aProduct.Id = Convert.ToInt32(aReader["Id"]);
                aProduct.Code = aReader["Code"].ToString();
                aProduct.Description = aReader["Description"].ToString();
                aProduct.Quantity = Convert.ToDouble(aReader["Quantity"]);
                productsList.Add(aProduct);
            }
            aConnection.Close();
            return productsList;

        }

        public int GetTotal()
        {
            string query = "SELECT SUM (Quantity) AS total FROM t_Product";
            aConnection.Open();
            SqlCommand aSqlCommand=new SqlCommand(query,aConnection);
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            aSqlDataReader.Read();
            int total=Convert.ToInt32(aSqlDataReader["total"]);
            aSqlDataReader.Close();
            aConnection.Close();
            return total;
            
        }
    }
}
     