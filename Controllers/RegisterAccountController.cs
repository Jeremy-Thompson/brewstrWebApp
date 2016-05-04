using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace brewstrWebApp.Controllers
{
    public class RegisterAccountController : Controller
    {
        //
        // GET: /RegisterAccount/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult User_Login(string username, string password, string email, string phone)
        {
            if (AuthenticateAccount(username, password, email, phone))
            {
                return Redirect("../User/Index");//View("~/Views/User/Index.cshtml");  
            }
            else
            {
                return View("~/Views//failedRegistration.cshtml");
            }
        }

        public bool AuthenticateAccount(string username, string password, string email, string phone)
        {
            string connectionString = null;

            // The register fields must contain something, otherwise no need to open database
            if (password == null || username == null || email == null || phone == null){
                // TODO: set the error message to please enter required fields
                return false;
            }
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            connectionString = "Data Source=(localdb)\\ProjectsV12;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False; Initial Catalog=Brewstr; User ID =admin;Password =admin";
            sql = "Select id,username,phone_number,email_address,password from CFG_USER where username = '" + username + "'";
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);

                // if the username already exists
                if (command.ExecuteReader() != null){
                    //TODO error message is username already exists
                    command.Dispose();
                    connection.Close();
                    return false;
                }
                // Insert new user information into table
                // TODO: How to get user id without using a third database call
                command.CommandText = "INSERT INTO CFG_USER [(username [, phone_number [, email_address [, password]]])] VALUES (" + username + " [, " + phone + "[," + email + "[, " + password + "]]])";
                command.ExecuteNonQuery();
                command.Dispose();
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
    }
}
