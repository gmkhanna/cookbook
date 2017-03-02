using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Cookbook
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            //Shows index page
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
            //Shows all styles of food recipes
            Get["/categories"] = _ => {
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };

            // Displays form to input new category
            Get["/categories/new"] = _ => {
                return View["category_new.cshtml"];
            };
            // Demonstrates that the add was successful
            Post["/categories/new"] = _ => {
                Category newCategory = new Category(Request.Form["category-name"]);
                newCategory.Save();
                List<Category> AllCategories = Category.GetAll();
                return View["categories.cshtml", AllCategories];
            };
            //Shows all recipes under that particular category
            Get["/recipes"] = _ => {
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };

            // Displays the form to add a new recipe
            Get["/recipes/new"] = _ => {
                return View["recipe_new.cshtml"];
            };

            // Demonstrates that the add for recipe was successful
            Post["/recipes/new"] = _ => {
                Recipe newRecipe = new Recipe(Request.Form["recipe-name"], Request.Form["recipe-ingredients"], Request.Form["recipe-instructions"], Request.Form["recipe-rating"]);
                newRecipe.Save();
                List<Recipe> AllRecipes = Recipe.GetAll();
                return View["recipes.cshtml", AllRecipes];
            };
        }
    }
}
