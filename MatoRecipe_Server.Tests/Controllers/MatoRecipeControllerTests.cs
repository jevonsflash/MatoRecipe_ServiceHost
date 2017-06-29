using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatoRecipe_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatoRecipe_Server.Controllers.Tests
{
    [TestClass()]
    public class MatoRecipeControllerTests
    {
        [TestMethod()]
        public void GetCookClassifyTest()
        {
            MatoRecipeController c = new MatoRecipeController();
            var result = c.GetCookClassify();

            Assert.IsNotNull(result);
        }
    }
}