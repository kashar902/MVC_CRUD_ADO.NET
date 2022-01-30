using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using crudApp.Models;

namespace crudApp.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = "Data Source=asharullah;Initial Catalog=MvcCrudDB;Integrated Security=True";
        private object productModel;

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Product", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            // TODO: Add insert logic here
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "INSERT INTO PRODUCT VALUES(@ProductName, @Price, @Count)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCommand.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCommand.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCommand.ExecuteNonQuery();

            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dataTableProduct = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "Select * from Product Where ProductID = @ProductID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("ProductID", id);
                sqlDataAdapter.Fill(dataTableProduct);
            }
            if (dataTableProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dataTableProduct.Rows[0][0].ToString());
                productModel.ProductName = dataTableProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dataTableProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dataTableProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Count = @Count Where ProductID = @ProductID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlCommand.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCommand.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCommand.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {

            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM PRODUCT WHERE ProductID = @ProductID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@ProductID", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
    }
}
