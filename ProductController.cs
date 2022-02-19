using ProductMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMVC.Controllers
{
    public class ProductController : Controller
    {
       
        SqlConnection connect = new SqlConnection();

        // GET: Product
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details()
        {
            List<ProductModel> obj = new List<ProductModel>();
            connect.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;";
            connect.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SelectDetails";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                booklist.Add(new BookModel { BookID = (int)dr["BookId"], BookName = (string)dr["BookName"], BookPrice = (int)dr["BookPrice"], BookAuthor = (string)dr["BookAuthor"] });
                obj.Add(new ProductModel { ProductID = (int)rdr["ProductID"], ProductName = (string)rdr["ProductName"], Rate = (decimal)rdr["Rate"], Description = (string)rdr["Description"], CategoryName = (String)rdr["CategoryName"] });
            }
            connect.Close();
            return View(obj);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel b1)
        {
            try
            {
                connect.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;";
                connect.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connect;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "AddData";
                cmd.Parameters.AddWithValue("@ProductName", b1.ProductName);
                cmd.Parameters.AddWithValue("@Rate", b1.Rate);
                cmd.Parameters.AddWithValue("@Description", b1.Description);
                cmd.Parameters.AddWithValue("@CategoryName", b1.CategoryName);
                cmd.ExecuteNonQuery();
                connect.Close();
                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            connect.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;";
            connect.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Editbyid";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            ProductModel b1 = new ProductModel() { ProductID = (int)rdr["ProductID"], ProductName = (string)rdr["ProductName"], Rate = (decimal)rdr["Rate"], Description = (string)rdr["Description"], CategoryName = (string)rdr["CategoryName"] };
            return View(b1);
        }


        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel b1)
        {
            connect.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True;Connect Timeout=30;";
            connect.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "UpdateById";
            cmd.Parameters.AddWithValue("@ProductID", b1.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", b1.ProductName);
            cmd.Parameters.AddWithValue("@Rate", b1.Rate);
            cmd.Parameters.AddWithValue("@Description", b1.Description);
            cmd.Parameters.AddWithValue("@CategoryName", b1.CategoryName);
            cmd.ExecuteNonQuery();
            
            return View("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
