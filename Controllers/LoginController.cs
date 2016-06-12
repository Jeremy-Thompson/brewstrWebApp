using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace brewstrWebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            //return View("~/Views/User.UserLanding.cshtml");
            return View("Index");
        }
        [HttpPost]
        public ActionResult User_Login(string Username, string Password)
        {
            if(AuthenticateAccount(Username, Password))
            {
                //return View("~/Views/User/UserLanding.cshtml");
                return Redirect("../User/Index"); 
            }
            else
            {
                return View("~/Views/Login/failedLogin.cshtml");
            }
        }
        [HttpPost]
        public ActionResult User_Register()
        {
            return View("~/Views/Login/FailedLogin.cshtml");

        }
        //[HttpPost]
        //public JsonResult MyAction(UserViewModel UserModel)
        //{
            /* do something with your usermodel object */
           // UserModel.Name = "foo";
           // UserModel.Age = 5;

          //  return Json(UserModel, JsonRequestBehavior.AllowGet);
        //}
        public bool AuthenticateAccount(string username, string password)
        {
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
            }
            catch (Exception ex)
            {
                Console.Write("Can not open connection ! ");
                Console.Write(ex.Message);
            }
            if ((r_password != null) && (r_username != null))
            {
                r_username = r_username.Trim();
                r_password = r_password.Trim();
                if ((password == r_password) && (username == r_username))
                {
                    TempData["username"] = r_username;
                    TempData["password"] = r_password;
                    TempData["id"] = r_id;
                    TempData["phone_number"] = r_phone_number;
                    TempData["email_address"] = r_email_address;
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}

