using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabTest03App.DAL;
using LabTest03App.DAL.DBGateway;

namespace LabTest03App.BLL
{
    internal class ProductManager
    {
        private ProductDBGateway aProductDbGateway = new ProductDBGateway();
        private const int MIN_LENGTH_OF_CODE = 3;

        public string Save(Product aProduct)
        {

            if (aProduct.Code.Length >= MIN_LENGTH_OF_CODE)
            {
                if (aProduct.Quantity > 0)
                {
                    Product aProductFound = aProductDbGateway.Find(aProduct.Code);

                    if (aProductFound == null)
                    {
                        aProductDbGateway.Save(aProduct);
                        return "Saved";
                    }
                    else
                    {
                        aProduct.Quantity += aProduct.Quantity;
                        int rowAffected = aProductDbGateway.Update(aProduct);
                        return rowAffected + "Product quantity updated Successfully!!";
                    }
                }
                else
                {
                    return "Quantity must be greater than zero!";
                }
            }
            else
            {
                return "Code must be " + MIN_LENGTH_OF_CODE + "char long";
            }

        }

        public List<Product> ShowAll()
        {
            return aProductDbGateway.ShowAll();
        }

        internal int GetTotalQuantity()
        {
            return aProductDbGateway.GetTotal();
        }
    }
}
   
