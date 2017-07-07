using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MatoRecipe_Model.Model;
using MatoRecipe_Server.Helper;

namespace MatoRecipe_Server.Controllers
{
    public class FoodDetailController : ApiController
    {
        [HttpGet]
        public FoodDetailEntity FoodDetail(string name)
        {
            FoodDetailEntity result = new FoodDetailEntity();
            var dbdata = DBHelper.Context.From<food_detail>().Where(c => c.name == name).ToFirstDefault();
            if (dbdata != null)
            {
                result = new FoodDetailEntity()
                {
                    Fcount = dbdata.fcount ?? 0,
                    Food = dbdata.food,
                    Id = dbdata.Id,
                    Img = dbdata.img,
                    Message = dbdata.message,
                    Name = dbdata.name,
                    Rcount = dbdata.rcount ?? 0,
                    Status = dbdata.status ?? true
                };
                result.Status = true;
            }
            return result;
        }

    }
}
