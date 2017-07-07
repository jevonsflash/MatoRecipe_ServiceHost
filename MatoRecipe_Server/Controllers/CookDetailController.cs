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
    public class CookDetailController : ApiController
    {
        [HttpGet]
        public CookDetailEntity CookDetail(int id)
        {
            CookDetailEntity result = new CookDetailEntity();
            var dbdata = DBHelper.Context.From<cook_detail>().Where(c => c.Id == id).ToFirstDefault();
            if (dbdata != null)
            {
                result = new CookDetailEntity()
                {
                    Count = dbdata.count,
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

        [HttpGet]
        public CookDetailEntity CookDetail(string name)
        {
            CookDetailEntity result = new CookDetailEntity();
            var dbdata = DBHelper.Context.From<cook_detail>().Where(c => c.name == name).ToFirstDefault();
            if (dbdata != null)
            {
                result = new CookDetailEntity()
                {
                    Count = dbdata.count,
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
