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
                Recipe_List.Add(new Recipe());
            }
        }
        public Recipe getRecipeByIndex(int index)
        {
           return Recipe_List.ElementAt(index);
        }
    }
}