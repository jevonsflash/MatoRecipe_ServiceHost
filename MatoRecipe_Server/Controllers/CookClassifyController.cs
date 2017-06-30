using MatoRecipe_Model.Model;
using MatoRecipe_Server.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MatoRecipe_Server.Controllers
{
    public class CookClassifyController : ApiController
    {
        [HttpGet]
        public CookClassifyEntity GetCookClassify()
        {
            CookClassifyEntity result = new CookClassifyEntity();
            var dbdata = DBHelper.Context.From<cook_classify>();
            if (dbdata != null)
            {
                result.Tngou = dbdata.ToEnumerable().Select(c => new CookClassify()
                {
                    Id = c.Id,
                    Cookclass = c.cook_class ?? 0,
                    Description = c.description,
                    Keywords = c.keywords,
                    Name = c.name,
                    Title = c.title,
                    Seq = c.seq ?? 0,


                }).ToList();
                result.Status = true;
            }
            return result;

        }
    }
}
