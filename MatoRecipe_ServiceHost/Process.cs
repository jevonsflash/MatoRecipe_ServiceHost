using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM;
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
            //var result = GetCookClassify();
            var result = GetCookShowItemClassifyId();


        }

        public async Task<ProcessResult> GetCookClassify()
        {

            var processresult = new ProcessResult();


            var clearresult = DBHelper.Context.DeleteAll<cook_classify>();
            processresult.Add(new ProcessResultItem(clearresult, ProcessResult.DB, ProcessResult.Succ));

            var data = await recipeServer.GetCookClassify();
            if (data.Status)
            {
                processresult.Add(new ProcessResultItem(data.Tngou.Count, ProcessResult.Get, ProcessResult.Succ));

            }
            else
            {
                processresult.Add(new ProcessResultItem(0, ProcessResult.Get, ProcessResult.Err));

                return processresult;
            }
            var datamodel = data.Tngou.Select(c => new cook_classify()
            {
                Id = c.Id,
                cook_class = c.Cookclass,
                description = c.Description,
                keywords = c.Keywords,
                name = c.Name,
                seq = 0,
                title = c.Title,

            }).ToList();
            try
            {
                var result = DBHelper.Context.Insert(datamodel);
                processresult.Add(new ProcessResultItem(result, "插入父类数据", ProcessResult.Succ));

            }
            catch (Exception e)
            {
                processresult.Add(new ProcessResultItem(0, "插入父类数据", e.Message));
                return processresult;
            }

            var topClassCookClassify = DBHelper.Context.From<cook_classify>().Where(c => c.cook_class == 0).ToList();
            if (topClassCookClassify != null && topClassCookClassify.Count > 0)
            {
                processresult.Add(new ProcessResultItem(topClassCookClassify.Count, ProcessResult.DB, ProcessResult.Succ));

                foreach (var item in topClassCookClassify)
                {
                    var subdata = await recipeServer.GetCookClassify(item.Id.ToString());
                    if (subdata.Status)
                    {
                        processresult.Add(new ProcessResultItem(subdata.Tngou.Count, ProcessResult.Get, ProcessResult.Succ));

                    }
                    else
                    {
                        processresult.Add(new ProcessResultItem(0, ProcessResult.Get, ProcessResult.Err));

                        continue;
                    }
                    var subdatamodel = subdata.Tngou.Select(c => new cook_classify()
                    {
                        Id = c.Id,
                        cook_class = c.Cookclass,
                        description = c.Description,
                        keywords = c.Keywords,
                        name = c.Name,
                        seq = 1,
                        title = c.Title,

                    }).ToList();
                    try
                    {
                        var result = DBHelper.Context.Insert(subdatamodel);
                        processresult.Add(new ProcessResultItem(result, "插入子类数据", ProcessResult.Succ));

                    }
                    catch (Exception e)
                    {
                        processresult.Add(new ProcessResultItem(0, "插入子类数据", e.Message));
                        continue;
                    }
                }

            }
            else
            {
                processresult.Add(new ProcessResultItem(0, ProcessResult.DB, ProcessResult.Err));

            }
            return processresult;
        }

        public async Task<ProcessResult> GetCookShowItem()
        {

            var processresult = new ProcessResult();


            var clearresult = DBHelper.Context.DeleteAll<cook_show_item>();
            processresult.Add(new ProcessResultItem(clearresult, ProcessResult.DB, ProcessResult.Succ));

            for (int i = 1; i < int.MaxValue; i++)
            {


                var data = await recipeServer.GetCookListByPage(i.ToString());
                if (data.Status)
                {
                    if (data.Tngou.Count == 0)
                    {
                        break;
                    }
                    processresult.Add(new ProcessResultItem(data.Tngou.Count, "请求菜谱数据，当前页码" + i, ProcessResult.Succ));

                }
                else
                {
                    processresult.Add(new ProcessResultItem(0, ProcessResult.Get, ProcessResult.Err));

                    continue;
                }
                var datamodel = data.Tngou.Select(c => new cook_show_item()
                {
                    Id = c.Id,
                    classify_id = 237,
                    count = c.Count,
                    //description = c.Description,
                    fcount = c.Fcount,
                    food = c.Food,
                    img = c.Img,
                    name = c.Name,
                    rcount = c.Rcount

                }).ToList();
                try
                {
                    var result = DBHelper.Context.Insert(datamodel);
                    processresult.Add(new ProcessResultItem(result, "菜谱数据插入DB，当前页码" + i, ProcessResult.Succ));

                }
                catch (Exception e)
                {
                    processresult.Add(new ProcessResultItem(0, "菜谱数据插入DB，当前页码" + i, e.Message));
                    continue;
                }
            }



            return processresult;
        }
        public async Task<ProcessResult> GetCookShowItemClassifyId()
        {

            var processresult = new ProcessResult();

            var subClassify = DBHelper.Context.From<cook_classify>().Where(c => c.seq == 1).ToList();

            foreach (var item in subClassify)
            {
                for (int i = 1; i < int.MaxValue; i++)
                {


                    var data = await recipeServer.GetCookListById(i.ToString(), item.Id.ToString());
                    if (data.Status)
                    {
                        if (data.Tngou.Count == 0)
                        {
                            break;
                        }
                        processresult.Add(new ProcessResultItem(data.Tngou.Count, "请求菜谱数据，当前类型" + item.Id + "，当前页码" + i, ProcessResult.Succ));

                    }
                    else
                    {
                        processresult.Add(new ProcessResultItem(0, ProcessResult.Get, ProcessResult.Err));

                        continue;
                    }
                    var datamodel = data.Tngou.Select(c => new cook_show_item()
                    {
                        Id = c.Id,
                        classify_id = item.Id,
                        count = c.Count,
                        //description = c.Description,
                        fcount = c.Fcount,
                        food = c.Food,
                        img = c.Img,
                        name = c.Name,
                        rcount = c.Rcount

                    }).ToList();

                    try
                    {
                        var result = DBHelper.Context.Update(datamodel);
                        processresult.Add(new ProcessResultItem(result, "菜谱数据插入DB，当前类型" + item.Id, ProcessResult.Succ));

                    }
                    catch (Exception e)
                    {
                        processresult.Add(new ProcessResultItem(0, "菜谱数据插入DB，当前类型" + item.Id, e.Message));
                        continue;
                    }


                }

            }

            return processresult;
        }

    }
}
