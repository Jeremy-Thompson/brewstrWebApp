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
            // The User_register action in registerAccount controller takes a User model from the form. This logic is to 
            // acoount for the user model tempdataand the User model parameters tempdata that the login form passes.
            User usr;
            if(TempData["user"] != null)
            {
                usr = (User)TempData["user"];
            }
            else
            {
                string username = (string) TempData["username"];
                string password = (string) TempData["password"];
                int id = (int)TempData["id"];
                string phone_number = (string)TempData["phone_number"];
                string email_address = (string) TempData["email_address"];
                int permissionLevel = 1;
                usr = new User(id, username, phone_number, email_address, password, permissionLevel);
            }
            // This is used in the _viewStart to determine which layout to use
            HttpContext.Session["Logged On"] = "true";
            StoreUserSessionState(usr);
            getUserInfo(usr);
            return View("~/Views/Home/Index.cshtml", usr);
        }

        public ActionResult ViewProfile()
        {
            try
            {
                String user_name = (String)Session["user_name"];
                int user_id = (int)Session["user_id"];
                String user_password = (String)Session["user_password"];
                String user_phone_number = (String)Session["user_phone_number"];
                String user_email_address = (String)Session["user_email_address"];
                int user_permission_level = (int)Session["user_permission_level"];

                User session_user = new User(user_id, user_name, user_phone_number, user_email_address, user_password, user_permission_level);

                return View("../User/Index", session_user);
            }catch( Exception ex)
            {
                return View("~/Views/Login/failedLogin.cshtml");
            }
        }

        public ActionResult SignOut()
        {
            User usr = new User();
            HttpContext.Session.Remove("Logged On");
            RemoveUserSessionState();
            return View("~/Views/Home/Index.cshtml", usr);
        }

        public void getUserInfo(User usr)
        {
            int r_Id = 0;
            string r_Name = null;
            string r_emailAddr = null;
            string r_password = null;
            string r_phoneNumber = null;


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
        public void StoreUserSessionState(User usr)
        {
            Session["user_name"] = usr.getUsername();
            Session["user_id"] = usr.getId();
            Session["user_password"] = usr.getPassword();
            Session["user_phone_number"] = usr.getPhoneNumber();
            Session["user_email_address"] = usr.getEmailAddress();
            Session["user_permission_level"] = usr.getPermissionLevel();
        }
        public void RemoveUserSessionState()
        {
            Session.Remove("user_id");
            Session.Remove("user_name");
            Session.Remove("user_phone_number");
            Session.Remove("user_email_address");
            Session.Remove("user_password");
            Session.Remove("user_permission_level");
        }
    }
}