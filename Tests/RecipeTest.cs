using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cookbook
{
    public class RecipeTest : IDisposable
    {
        public RecipeTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cookbook_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Recipe firstRecipe = new Recipe("Salmon", "Salmon", "Boil", "3");
            firstRecipe.Save();

            //Act
            Recipe savedRecipe = Recipe.GetAll()[0];

            int result = savedRecipe.GetId();
            int testId = firstRecipe.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
       public void Test_FindFindsRecipeInDatabase()
       {
           //Arrange
           Recipe firstRecipe = new Recipe("Salmon", "Salmon", "Boil", "3");
           firstRecipe.Save();

           //Act
           Recipe result = Recipe.Find(firstRecipe.GetId());

           //Assert
           Assert.Equal(firstRecipe, result);
       }

       [Fact]
        public void Test_AddCategory_AddsCategoryToRecipe()
        {
            //Arrange
            Recipe testRecipe = new Recipe("Salmon", "Salmon", "Boil", "3");
            testRecipe.Save();

            Category testCategory = new Category("Soup");
            testCategory.Save();

            //Act
            testRecipe.AddCategory(testCategory);

            List<Category> result = testRecipe.GetCategories();
            List<Category> testList = new List<Category>{testCategory};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Recipe.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
