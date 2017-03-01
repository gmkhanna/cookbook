using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cookbook
{
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cookbook_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Category firstCategory = new Category("Curry");
            firstCategory.Save();

            //Act
            Category savedCategory = Category.GetAll()[0];

            int result = savedCategory.GetId();
            int testId = firstCategory.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
       public void Test_Find_FindsCategoryInDatabase()
       {
           //Arrange
           Category firstCategory = new Category("Salmon");
           firstCategory.Save();

           //Act
           Category result = Category.Find(firstCategory.GetId());

           //Assert
           Assert.Equal(firstCategory, result);
       }

       [Fact]
        public void Test_AddCategory_AddsCategoryToCategory()
        {
            //Arrange
            Category testCategory = new Category("Spicy");
            testCategory.Save();

            Recipe testRecipe = new Recipe("Salmon", "Salmon", "Boil", "3");
            testRecipe.Save();

            //Act
            testCategory.AddRecipe(testRecipe);

            List<Recipe> result = testCategory.GetRecipes();
            List<Recipe> testList = new List<Recipe>{testRecipe};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Category.DeleteAll();
            Recipe.DeleteAll();
        }
    }
}
