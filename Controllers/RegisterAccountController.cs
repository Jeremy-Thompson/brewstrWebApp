using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Sql;

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
                return View("~/Views/RegisterAccount/failedRegistration.cshtml");
            }
        }

        public bool InsertAccount(string username, string password, string email, string phone)
        {
            // The register fields must contain something, otherwise no need to open database
            if (password == null || username == null || email == null || phone == null){
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
                if((r_email_address != null))
                {
                    return false;
                }
                // Insert new user information into table
                // TODO: How to get user id without using a third database call
                connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("INSERT INTO CFG_USER (name,username,phone_number,email_address,password) VALUES (@name, @username, @phone_number, @email_address, @password)");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@name", username);
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
            TempData["id"] = 7;
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
