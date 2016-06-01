using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Globalization;
using brewstrWebApp.Models;
using System.Text.RegularExpressions;

namespace brewstrWebApp.Controllers
{
    public class RegisterAccountController : Controller
    {
        //
        // GET: /RegisterAccount/
        bool invalid = false;
        bool usernameExists = false;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult User_Register(User user)
        {
            TempData["user"] = user; 
            return Redirect("../User/Index");//View("~/Views/User/Index.cshtml");  
        }

        public bool InsertAccount(string name, string username, string password, string email, string phone)
        {
            // The register fields must contain something, otherwise no need to open database
            if (password == null || username == null || email == null || phone == null)
            {
                // TODO: set the error message to please enter required fields
                return false;
            }
            string r_username = null;
            string r_password = null;
            string r_phone_number = null;
            string r_email_address = null;
            int r_id = 0;

            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connectionString = "Data Source=(localdb)\\ProjectsV12;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False; Initial Catalog=Brewstr; User ID =admin;Password =admin";
            sql = "Select id,username,phone_number,email_address,password from CFG_USER where username = '" + username + "'";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();

                //Check for a matching account in the database
                while (dataReader.Read())
                {
                    r_id = dataReader.GetInt32(0);
                    r_username = dataReader.GetString(1);
                    r_phone_number = dataReader.GetString(2);
                    r_email_address = dataReader.GetString(3);
                    r_password = dataReader.GetString(4);

                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                // if the username already exists
                if ((r_username != null))
                {
                    usernameExists = true;
                    return false;
                }
                // Insert new user information into table
                // TODO: How to get user id without using a third database call
                connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("INSERT INTO CFG_USER (name,username,phone_number,email_address,password) VALUES (@name, @username, @phone_number, @email_address, @password)");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@phone_number", phone);
                cmd.Parameters.AddWithValue("@email_address", email);
                cmd.Parameters.AddWithValue("@password", password);
                connection.Open();
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.Write("Can not open connection ! ");
                Console.Write(ex.Message);
            }
            TempData["username"] = username;
            TempData["password"] = password;
            TempData["id"] = 1;
            TempData["phone_number"] = phone;
            TempData["email_address"] = email;
            return true;
        }

        /*
         * Checks that the username entered in the username field is unique
         * The checks are triggered everytime a new character is entered into the field
         */
        public JsonResult uniqueUsername()
        {
            // Retrieves the esername entry from the field
            string username = Request.QueryString.Get(0);

            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connectionString = "Data Source=(localdb)\\ProjectsV12;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False; Initial Catalog=Brewstr; User ID =admin;Password =admin";
            sql = "Select username from CFG_USER where username = '" + username + "'";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                if(dataReader.HasRows)
                {
                    return Json(username + " is not available", JsonRequestBehavior.AllowGet);;
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Can not open connection ! ");
                Console.Write(ex.Message);
                return Json("error in accessing DB", JsonRequestBehavior.AllowGet);
            }
        }
    }
}