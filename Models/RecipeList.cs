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
            populateListWithBS();
        }

        public void populateListWithBS()
        {
            for (int i = 0; i < 10; i++)
            {
                Recipe_List.Add(new Recipe(String.Format("Test Name:",i),String.Format("Test Author:",i), DateTime.Now,i,i,i,i,i,i,i,i,i,i));
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