using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using brewstrWebApp.Models.Recipe;
using System.Data.Sql;
using System.Data.SqlClient;

namespace brewstrWebApp.Controllers
{
    public class RecipeListController : Controller
    {
        //
        // GET: /RecipeList/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string search_str)
        {
            List<brewstrWebApp.Models.Recipe> Recipe_List = Search_Recipe_By_Name(search_str);
            return View();
        }
        public brewstrWebApp.Models.RecipeList Search_Recipe_By_Name(String name)
        {
            return new brewstrWebApp.Models.RecipeList();
        }
        public brewstrWebApp.Models.RecipeList Search_Recipe_By_Category(int category)
        {

            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            String connectionString = "Data Source=(localdb)\\ProjectsV12;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False; Initial Catalog=Brewstr; User ID =admin;Password =admin";
            sql = "Select Id,name,description, author, brew_count from DYN_RECIPE where category = '" + category + "'";
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

        }
  

    }
}
