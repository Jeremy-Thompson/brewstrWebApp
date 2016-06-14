using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace brewstrWebApp.Models
{
    public class RecipeList
    {
        public List<Recipe> Recipe_List;
        public RecipeList()
        {
            Recipe_List = new List<Recipe>();
            populateListWithBS();
        }

        public void populateListWithBS()
        {
            Recipe rp;
            for (int i = 0; i < 5; i++)
            {
                rp = new Recipe(String.Format("Test Name:",i),String.Format("Test Author:",i), DateTime.Now,i,i,i,i,i,i,i,i,i,i);
                Recipe_List.Add(rp);
            }
        }
        public Recipe getRecipeByIndex(int index)
        {
           return Recipe_List.ElementAt(index);
        }
        public int getRecipeListSize()
        {
            return Recipe_List.Count;
        }
    }
}