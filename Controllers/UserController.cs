using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using brewstrWebApp.Models;
using System.Data.SqlClient;
using System.Web.UI;
using brewstrWebApp.Filters;

namespace brewstrWebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        // The LoadUserLayoutFilter changes the Layout file so as to swap the "register" and "login" options for a User dropdown option in the navbar
        [LoadUserLayout]
        public ActionResult Index()
        {
            string username = (string) TempData["username"];
            string password = (string) TempData["password"];
            int id = (int)TempData["id"];
            string phone_number = (string)TempData["phone_number"];
            string email_address = (string) TempData["email_address"];
            bool isAdmin = username.Substring(0,1).Equals("j");
            User usr = new User(id, username, phone_number, email_address, password, isAdmin);
            // This is used in the _viewStart to determine which layout to use
            HttpContext.Session["Logged On"] = "true";
            getUserInfo(usr);
            return View("~/Views/Home/Index.cshtml", usr);
        }

        public ActionResult SignOut()
        {
            User usr = new User();
            HttpContext.Session.Remove("Logged On");
            return View("~/Views/Home/Index.cshtml", usr);
        }

        public void getUserInfo(User usr)
        {
            int r_Id = 0;
            string r_Name = null;
            string r_emailAddr = null;
            string r_password = null;
            string r_phoneNumber = null;
            int r_moduleId = 0;
            string r_moduleType = null;

            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connectionString = "Data Source=(localdb)\\ProjectsV12;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False; Initial Catalog=Brewstr; User ID =admin;Password =admin";
            sql = "SELECT CFG_USER.id,CFG_USER.name,CFG_USER.phone_number,CFG_USER.email_address,CFG_USER.password, CFG_USER_DEVICES.device_id, CFG_USER_DEVICES.device_type FROM CFG_USER Left Join CFG_USER_DEVICES On [CFG_USER].id = [CFG_USER_DEVICES].user_id where username = '" + usr.getUsername() + "'";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    r_Id = dataReader.GetInt32(0);
                    r_Name = dataReader.GetString(1);
                    r_phoneNumber = dataReader.GetString(2);
                    r_emailAddr = dataReader.GetString(3);
                    r_password = dataReader.GetString(4);
                    r_moduleId = (dataReader.GetInt32(5) == null ? 0 : dataReader.GetInt32(5));
                    r_moduleType = dataReader.GetString(6);
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.Write("Can not open connection ! ");
                Console.Write(ex.Message);
            }
        }
    }
}