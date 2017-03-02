using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Cookbook
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
            Get["/categories"] = _ => {
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };
            Get["/categories/new"] = _ => {
                return View["category_new.cshtml"];
            };
            Post["/categories/new"] = _ => {
                Category newCategory = new Category(Request.Form["category-name"]);
                newCategory.Save();
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };
            Get["/recipes"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            Get["/recipes/new"] = _ => {
                return View["recipe_new.cshtml"];
            };

            Post["/recipes/new"] = _ => {
                Recipe newRecipe = new Recipe(Request.Form["recipe-name"], Request.Form["recipe-ingredients"], Request.Form["recipe-instructions"], Request.Form["recipe-rating"]);
                newRecipe.Save();
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };
        }
    }
}
