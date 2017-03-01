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
