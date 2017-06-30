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
    public class CookListControllerTests
    {
        [TestMethod()]
        public void GetCookListTest()
        {
            CookListController c = new CookListController();
            var result = c.GetCookList(1, 20);
            Assert.IsNotNull(result);
            
        }
    }
}