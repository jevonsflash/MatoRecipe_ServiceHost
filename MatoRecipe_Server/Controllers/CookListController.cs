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
    public class CookListController : ApiController
    {

        [HttpGet]
        public CookListEntity CookList()
        {
            CookListEntity result = new CookListEntity();
            var dbdata = DBHelper.Context.From<cook_show_item>().OrderBy(c => c.Id).Page(20, 1);
            if (dbdata != null)
            {
                result.Tngou = dbdata.ToEnumerable().Select(c => new CookListItem()
                {
                    Id = c.Id,
                    Count = c.count ?? 0,
                    Fcount = c.fcount ?? 0,
                    Food = c.food,
                    Img = c.img,
                    Name = c.name,
                    Rcount = c.rcount ?? 0

                }).ToList();
                result.Status = true;
            }
            return result;

        }
        [HttpGet]
        public CookListEntity CookList(string name)
        {
            CookListEntity result = new CookListEntity();
            var dbdata = DBHelper.Context.From<cook_show_item>().Where(c => c.name.Contains(name));
            if (dbdata != null)
            {
                result.Tngou = dbdata.ToEnumerable().Select(c => new CookListItem()
                {
                    Id = c.Id,
                    Count = c.count ?? 0,
                    Fcount = c.fcount ?? 0,
                    Food = c.food,
                    Img = c.img,
                    Name = c.name,
                    Rcount = c.rcount ?? 0

                }).ToList();
                result.Status = true;
            }
            return result;

        }
        [HttpGet]
        public CookListEntity CookList(int pageIndex, int rowCount)
        {
            CookListEntity result = new CookListEntity();
            var dbdata = DBHelper.Context.From<cook_show_item>().Page(rowCount, pageIndex);
            if (dbdata != null)
            {
                result.Tngou = dbdata.ToEnumerable().Select(c => new CookListItem()
                {
                    Id = c.Id,
                    Count = c.count ?? 0,
                    Fcount = c.fcount ?? 0,
                    Food = c.food,
                    Img = c.img,
                    Name = c.name,
                    Rcount = c.rcount ?? 0

                }).ToList();
                result.Status = true;
            }
            return result;

        }
    }
}
