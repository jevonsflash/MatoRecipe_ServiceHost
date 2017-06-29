using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatoRecipe_Generator.Helper;
using MatoRecipe_Generator.Server;
using MatoRecipe_Model.Model;

namespace MatoRecipe_Generator
{
    public class Process
    {
        RecipeServer recipeServer = new RecipeServer();
        public Process()
        {

        }

        public void Start()
        {
            GetCookClassify();
            Console.WriteLine("done");
        }

        public async Task<bool> GetCookClassify()
        {
            var data = await recipeServer.GetCookClassify();
            var datamodel = data.Tngou.Select(c => new cook_classify()
            {
                Id = c.Id,
                cook_class = c.Cookclass,
                description = c.Description,
                keywords = c.Keywords,
                name = c.Name,
                seq = c.Seq,
                title = c.Title,

            }).ToList();
            try
            {
                var result = DBHelper.Context.Insert(datamodel);
                return result > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
