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
              { "http://cssdeck.com/uploads/media/items/2/2v3VhAp.png", 
                "http://cssdeck.com/uploads/media/items/1/1swi3Qy.png", 
                "http://cssdeck.com/uploads/media/items/6/6f3nXse.png", 
                "http://cssdeck.com/uploads/media/items/8/8kEc1hS.png",
                "http://cssdeck.com/uploads/media/items/1/1swi3Qy.png", 
                "http://cssdeck.com/uploads/media/items/6/6f3nXse.png" };

            TempData["beerName"] = new string[] 
              { "Oldknow's Nutcracker", 
                "epic IPA", 
                "Blonde Pale Ale", 
                "Nutmeg Stout", 
                "Island Lager", 
                "Ray's Raspberry" };
            RecipeList recipe_list = new RecipeList();

            return View(recipe_list);
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
