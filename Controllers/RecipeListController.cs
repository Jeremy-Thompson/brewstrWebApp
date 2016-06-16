using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using brewstrWebApp.Models;

namespace brewstrWebApp.Controllers
{
    public class RecipeListController : Controller
    {
        //
        // GET: /RecipeList/

        public ActionResult Index()
        {
            TempData["imageSource"] = new string[] 
              { "/Content/images/sunriseIPA.png", 
                "/Content/images/jimsPaleAle.png", 
                "/Content/images/nutmegStout.png", 
                "/Content/images/bruxellesBlonde.png",
                "/Content/images/sunriseIPA.png", 
                "http://cssdeck.com/uploads/media/items/6/6f3nXse.png" };

            TempData["beerName"] = new string[] 
              { "Start your morning dry but with a great body", 
                "Average Ale for Average guys", 
                "Put yourself in the Chistmas spirit with this festive stout", 
                "Lovely sweet blonde (We should all be so lucky)", 
                "Start your morning dry but with a great body.... Again", 
                "Ray's Raspberry" };
            List<Recipe> recipe_list = new List<Recipe>();
            Recipe rp;
            for (int i = 0; i < 5; i++)
            {
                rp = new Recipe(String.Format("Test Name:", i), String.Format("Test Author:", i), DateTime.Now, i, i, i, i, i, i, i, i, i, i);
                recipe_list.Add(rp);
            }
            IEnumerable<Recipe> re = recipe_list;
            return View(re);
        }
        [HttpPost]
        public ActionResult Search(string search_str)
        {
            brewstrWebApp.Models.RecipeList Recipe_List = Search_Recipe_By_Name(search_str);
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
                   /*
                    r_id = dataReader.GetInt32(0);
                    r_username = dataReader.GetString(1);
                    r_phone_number = dataReader.GetString(2);
                    r_email_address = dataReader.GetString(3);
                    r_password = dataReader.GetString(4);
                    * */
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                return new brewstrWebApp.Models.RecipeList();
            }
            catch (Exception ex)
            {
                Console.Write("Can not open connection ! ");
                Console.Write(ex.Message);
                return new brewstrWebApp.Models.RecipeList();
            }

        }


    }
}
