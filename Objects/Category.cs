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
