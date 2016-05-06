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
        public ActionResult User_Register(string username, string password, string email, string phone)
        {
            string usernameTrim = username.Trim();
            string passwordTrim = password.Trim();
            string emailTrim = email.Trim();
            string phoneTrim = phone.Trim();
            if ((InputValidation(usernameTrim, passwordTrim, emailTrim, phoneTrim) < 0) && 
                InsertAccount(usernameTrim, passwordTrim, emailTrim, phoneTrim))
            {
                return Redirect("../User/Index");//View("~/Views/User/Index.cshtml");  
            }
            else
            {
                return View("~/Views//failedRegistration.cshtml");
            }
        }

        public bool InsertAccount(string username, string password, string email, string phone)
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
                int rows = command.ExecuteNonQuery();
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
            TempData["phone_number"] = phone;
            TempData["email_address"] = email;
            return true;
        }

        int InputValidation(string username, string password, string email, string phone)
        {
            /*
             * Message Flag Map
             * Bit 1: username is too long
             * Bit 2: password is too long
             * Bit 3: password is too short
             * Bit 4: phone number is too long
             * Bit 5: email is too long
             */
            int messageFlag = 0;
            // The register fields must contain something, otherwise no need to open database
            if (username.Length > 100)
            {
                messageFlag += 1;
            }
            if (password.Length > 30)
            {
                messageFlag += 2;
            }
            if (password.Length < 5)
            {
                messageFlag += 4;
            }
            if (phone.Length > 16)
            {
                messageFlag += 8;
            }
            if (email.Length > 30)
            {
                messageFlag += 16;
            }
            return messageFlag;
        }
    }
}
