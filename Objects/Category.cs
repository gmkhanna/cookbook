using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Cookbook
{
    public class Category
    {
        private int _id;
        private string _style;

        public Category(string Style, int Id = 0)
        {
            _id = Id;
            _style = Style;
        }
        public override bool Equals(System.Object otherCategory)
        {
            if(!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category) otherCategory;
                bool idEquality = this.GetId() == newCategory.GetId();
                bool styleEquality = this.GetStyle() == newCategory.GetStyle();
                return (idEquality && styleEquality);
            }
        }

        public static List<Category> GetAll()
        {
            List<Category> AllCategories = new List<Category> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string categoryStyle = rdr.GetString(1);

                Category newCategory = new Category(categoryStyle, categoryId);
                AllCategories.Add(newCategory);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return AllCategories;
        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO categories (style) OUTPUT INSERTED.id VALUES (@Style)", conn);

            SqlParameter styleParameter = new SqlParameter("@Style", this.GetStyle());
            cmd.Parameters.Add(styleParameter);

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

        public static Category Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId", conn);

            SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", id.ToString());
            cmd.Parameters.Add(categoryIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCategoryId = 0;
            string foundCategoryStyle = null;

            while(rdr.Read())
            {
                foundCategoryId = rdr.GetInt32(0);
                foundCategoryStyle = rdr.GetString(1);
            }

            Category foundCategory = new Category(foundCategoryStyle, foundCategoryId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundCategory;
        }

        public void AddRecipe(Recipe newRecipe)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cookbook (recipe_id, category_id) VALUES (@RecipeId, @CategoryId);", conn);

            SqlParameter idRecipeParam = new SqlParameter("@RecipeId", newRecipe.GetId());
            SqlParameter idCategoryParam = new SqlParameter("@CategoryId", this.GetId());

            cmd.Parameters.Add(idRecipeParam);
            cmd.Parameters.Add(idCategoryParam);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }

        }
            public List<Recipe> GetRecipes()
            {
                SqlConnection conn = DB.Connection();
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT recipes.* FROM categories JOIN cookbook ON (categories.id = cookbook.category_id) JOIN recipes ON (cookbook.recipe_id = recipes.id) WHERE categories.id = @CategoryId;", conn);

                SqlParameter idCategoryParam = new SqlParameter("@CategoryId", this.GetId().ToString());

                cmd.Parameters.Add(idCategoryParam);

                List<Recipe> recipeList = new List<Recipe> {};

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int recipeId = rdr.GetInt32(0);
                    string recipeName = rdr.GetString(1);
                    string recipeInstructions = rdr.GetString(2);
                    string recipeIngredients = rdr.GetString(3);
                    string recipeRating = rdr.GetString(4);
                    Recipe newRecipe = new Recipe(recipeName, recipeInstructions, recipeIngredients, recipeRating, recipeId);
                    recipeList.Add(newRecipe);
                }

                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
                return recipeList;
            }

            public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id=@CategoryId;", conn);

            SqlParameter idParameter = new SqlParameter("@CategoryId", this.GetId());
            cmd.Parameters.Add(idParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Update(string newStyle)
      {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("UPDATE categories SET style = @NewStyle OUTPUT INSERTED.style WHERE id = @CategoryId;", conn);

          SqlParameter newStyleParameter = new SqlParameter();
          newStyleParameter.ParameterName = "@NewStyle";
          newStyleParameter.Value = newStyle;
          cmd.Parameters.Add(newStyleParameter);

          SqlParameter categoryIdParameter = new SqlParameter("@CategoryId", this.GetId());
          cmd.Parameters.Add(categoryIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
              this._style = rdr.GetString(0);
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
            SqlCommand cmd = new SqlCommand("DELETE FROM categories;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public int GetId()
        {
            return _id;
        }
        public string GetStyle()
        {
            return _style;
        }
        public void SetStyle(string newStyle)
        {
            _style = newStyle;
        }
    }
}
