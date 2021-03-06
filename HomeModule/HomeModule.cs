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
      //Finds the proper category, and lists the recipes within it. Recipes can be added to this category from here.
      Get["/category/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category SelectedCategory = Category.Find(parameters.id);
        List<Recipe> CategoryRecipes = SelectedCategory.GetRecipes();
        List<Recipe> AllRecipes = Recipe.GetAll();
        model.Add("category", SelectedCategory);
        model.Add("categoryRecipes", CategoryRecipes);
        model.Add("allRecipes", AllRecipes);
        return View["category.cshtml", model];
      };

      Get["/category/{id}/edit"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        return View["category_edit_form.cshtml", SelectedCategory];
      };

      Patch["/category/{id}/updated"] = parameters => {
        Category selectedCategory = Category.Find(parameters.id);
        selectedCategory.Update(Request.Form["category-style"]);
        return View["category_updated.cshtml"];
      };

      Delete["/categories/{id}/deleted"] = parameters => {
        Category selectedCategory = Category.Find(parameters.id);
        selectedCategory.Delete();
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      };

      //Add a recipe to a category
      Post["/recipe_added"] = _ => {
        Recipe recipe = Recipe.Find(Request.Form["recipe-id"]);
        Category category = Category.Find(Request.Form["category-id"]);
        category.AddRecipe(recipe);
        return View["success.cshtml"];
      };
      //Shows all recipes under that particular category; Gives you option to add a recipe
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
      //Find targeted recipe and all associated categories. Recipe can be added to category.
      Get["/recipes/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Recipe SelectedRecipe = Recipe.Find(parameters.id);
        List<Category> RecipeCategories = SelectedRecipe.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        model.Add("recipe", SelectedRecipe);
        model.Add("recipeCategories", RecipeCategories);
        model.Add("allCategories", AllCategories);
        return View["recipe.cshtml", model];
      };
      Get["/recipe/{id}/edit"] = parameters => {
        Recipe SelectedRecipe = Recipe.Find(parameters.id);
        return View["recipe_edit_form.cshtml", SelectedRecipe];
      };

      Patch["/recipe/{id}/updated"] = parameters => {
        Recipe selectedRecipe = Recipe.Find(parameters.id);
        selectedRecipe.Update(Request.Form["recipe-name"]);
        return View["category_updated.cshtml"];
      };

      Delete["/recipe/{id}/deleted"] = parameters => {
        Recipe selectedRecipe = Recipe.Find(parameters.id);
        selectedRecipe.Delete();
        List<Recipe> AllRecipes = Recipe.GetAll();
        return View["recipes.cshtml", AllRecipes];
      };

      //This will add a category to te recipe ; then show success page once added.
      Post["/category_added"] = _ => {
        Recipe recipe = Recipe.Find(Request.Form["recipe-id"]);
        Category category = Category.Find(Request.Form["category-id"]);
        recipe.AddCategory(category);
        return View["success.cshtml"];
      };

      Post["/searched_recipes"] = _ => {
        List<Recipe> MatchedRecipes = Recipe.SearchedIngredient(Request.Form["searched-recipes"]);
        return View["recipes_by_ingredient.cshtml", MatchedRecipes];
      };
    }
  }
}
