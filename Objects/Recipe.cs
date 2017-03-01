using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cookbook
{
    public class Recipe
    {
        private int _id;
        private string _name;
        private string _ingredients;
        private string _instructions;
        private string _rating;

        public Recipe(string Name, string Ingredients, string Instructions, string Rating, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _ingredients = Ingredients;
            _instructions = Instructions;
            _rating = Rating;
        }
        public override bool Equals(System.Object otherRecipe)
        {
            if(!(otherRecipe is Recipe))
            {
                return false;
            }
            else
            {
                Recipe newRecipe = (Recipe) otherRecipe;
                bool idEquality = this.GetId() == newRecipe.GetId();
                bool nameEquality = this.GetName() == newRecipe.GetName();
                bool ingredientsEquality = this.GetIngredients() == newRecipe.GetIngredients();
                bool instructionsEquality = this.GetInstructions() == newRecipe.GetInstructions();
                bool ratingEquality = this.GetRating() == newRecipe.GetRating();
                return (idEquality && nameEquality && ingredientsEquality && instructionsEquality &&ratingEquality);
            }
        }

        public static List<Recipe> GetAll()
        {
            List<Recipe> AllRecipes = new List<Recipe> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM recipes;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int recipeId = rdr.GetInt32(0);
                string recipeName = rdr.GetString(1);
                string recipeIngredients = rdr.GetString(2);
                string recipeInstructions = rdr.GetString(3);
                string rating = rdr.GetString(4);

                Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, rating, recipeId);
                AllRecipes.Add(newRecipe);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return AllRecipes;
        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO recipes (name, ingredients, instructions, rating) OUTPUT INSERTED.id VALUES (@RecipeName, @RecipeIngredients, @RecipeInstructions, @RecipeRating)", conn);

            SqlParameter styleParameter = new SqlParameter("@RecipeName", this.GetName());
            SqlParameter ingredientsParameter = new SqlParameter("@RecipeIngredients", this.GetIngredients());
            SqlParameter instructionsParameter = new SqlParameter("@RecipeInstructions", this.GetInstructions());
            SqlParameter ratingParameter = new SqlParameter("@RecipeRating", this.GetRating());

            cmd.Parameters.Add(styleParameter);
            cmd.Parameters.Add(ingredientsParameter);
            cmd.Parameters.Add(instructionsParameter);
            cmd.Parameters.Add(ratingParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Recipe Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM recipes WHERE id = @RecipeId", conn);

            SqlParameter recipeIdParameter = new SqlParameter("@RecipeId", id.ToString());

            cmd.Parameters.Add(recipeIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundRecipeId = 0;
            string foundRecipeName = null;
            string foundRecipeIngredients = null;
            string foundRecipeInstructions = null;
            string foundRecipeRating = null;

            while(rdr.Read())
            {
                foundRecipeId = rdr.GetInt32(0);
                foundRecipeName = rdr.GetString(1);
                foundRecipeIngredients = rdr.GetString(2);
                foundRecipeInstructions = rdr.GetString(3);
                foundRecipeRating = rdr.GetString(4);
            }
            Recipe foundRecipe = new Recipe(foundRecipeName, foundRecipeIngredients, foundRecipeInstructions, foundRecipeRating, foundRecipeId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundRecipe;
        }

        public void AddCategory(Category newCategory)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cookbook (recipe_id, category_id) VALUES (@RecipeId, @CategoryId)", conn);

            SqlParameter recipeIdParameter = new SqlParameter("@RecipeId", this.GetId());
            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", newCategory.GetId());

            cmd.Parameters.Add(recipeIdParameter);
            cmd.Parameters.Add(categoryIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null);
            {
                conn.Close();
            }
        }

        public List<Category> GetCategories()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT categories.* FROM recipes JOIN cookbook ON (recipes.id = cookbook.recipe_id) JOIN categories ON (cookbook.category_id = categories.id) WHERE recipes.id = @RecipeId;", conn);

            SqlParameter recipeIdParameter = new SqlParameter("@RecipeId", this.GetId().ToString());

            cmd.Parameters.Add(recipeIdParameter);


            List<Category> categoryList = new List<Category> {};

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int CategoryId = rdr.GetInt32(0);
                string CategoryStyle = rdr.GetString(1);

                Category newCategory = new Category(CategoryStyle, CategoryId);
                categoryList.Add(newCategory);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return categoryList;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM recipes;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string newName)
        {
            _name = newName;
        }
        public string GetIngredients()
        {
            return _ingredients;
        }

        public void SetIngredients(string newIngredients)
        {
            _ingredients = newIngredients;
        }
        public string GetInstructions()
        {
            return _instructions;
        }

        public void SetInstructions(string newInstructions)
        {
            _instructions = newInstructions;
        }
        public string GetRating()
        {
            return _rating;
        }

        public void SetRating(string newRating)
        {
            _rating = newRating;
        }
    }
}
