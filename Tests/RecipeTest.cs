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
        public void Dispose()
        {
            Recipe.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
