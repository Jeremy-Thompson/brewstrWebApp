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
        public ActionResult User_Register(string name, string username, string password, string email, string phone)
        {
            string usernameTrim = username.Trim();
            string passwordTrim = password.Trim();
            string emailTrim = email.Trim();
            string phoneTrim = phone.Trim();
            int errorMsg = InputValidation(usernameTrim, passwordTrim, emailTrim, phoneTrim);
            bool insertAccount = InsertAccount(name, usernameTrim, passwordTrim, emailTrim, phoneTrim);

            if ((errorMsg < 1) && insertAccount)
            {
                return Redirect("../User/Index");//View("~/Views/User/Index.cshtml");  
            }
            else
            {
                RegistrationError regErr = new RegistrationError(errorMsg, usernameExists);
                return View("~/Views/RegisterAccount/failedRegistration.cshtml", regErr);
            }
        }

        public bool InsertAccount(string name, string username, string password, string email, string phone)
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
                if((r_username != null))
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

        int InputValidation(string username, string password, string email, string phone)
            {
            /*
             * Message Flag Map
             * Bit 1: username is too long
             * Bit 2: password is too long
             * Bit 3: password is too short
             * Bit 4: phone number is too long
             * Bit 5: email is too long
             * Bit 6: email is invalid
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
            if (!IsValidEmail(email))
            {
                messageFlag += 32;
            }
            return messageFlag;
        }

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return Create();
        }
    }
}