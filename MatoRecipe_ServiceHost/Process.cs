using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


            int i = 0;
            var maintask = GetCookDetail();
            while (!maintask.IsCompleted)
            {
                Thread.Sleep(1000);
                i++;
            }
            Console.WriteLine("第一进程执行完毕");
            Console.WriteLine("总耗时{0}秒", i);
            i = 0;
            var subtask = GetFoodDetail();

            while (!subtask.IsCompleted)
            {
                Thread.Sleep(1000);
                i++;
            }
            Console.WriteLine("第二进程执行完毕");
            Console.WriteLine("总耗时{0}秒", i);

        }

        private void Process1()
        {
            Console.WriteLine("主进程开始");

            int i = 0;
            var maintask = GetCookShowItem();
            while (!maintask.IsCompleted)
            {
                Thread.Sleep(1000);
                i++;
            }
            Console.WriteLine("主进程执行完毕");
            Console.WriteLine("总耗时{0}秒", i);
            i = 0;
            Console.WriteLine("补录进程开始", i);

            var mainResult = maintask.Result;
            var errorResult = mainResult.Log.Where(c => c.Result == ProcessResultType.失败);


            var reprocessPageIndex = errorResult.Select(c => c.Param[0].Substring(3)).ToList();
            var retask = ReGetCookList(reprocessPageIndex);
            while (!retask.IsCompleted)
            {
                Thread.Sleep(1000);
                i++;
            }
            Console.WriteLine("补录程执行完毕");
            Console.WriteLine("总耗时{0}秒", i);


            var aa = GetShowItemClassify();
        }



        /// <summary>
        /// 获取菜谱分类
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessResult> GetCookClassify()
        {

            var processresult = new ProcessResult();


            var clearresult = DBHelper.Context.DeleteAll<cook_classify>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "cook_classify" });
            CookClassifyEntity data = null;
            try
            {
                data = await recipeServer.GetCookClassify();

            }
            catch (Exception e)
            {
                processresult.Add("一级分类数据获取或解析", ProcessResultType.失败, e.Message);

            }
            if (data != null && data.Status)
            {
                processresult.Add("一级分类数据验证", ProcessResultType.成功);

            }
            else
            {
                processresult.Add("一级分类数据验证", ProcessResultType.失败);

                return processresult;
            }
            var datamodel = data.Tngou.Select(c => new cook_classify()
            {
                Id = c.Id,
                cook_class = c.Cookclass,
                description = c.Description,
                name = c.Name,
                seq = 0,
                title = c.Title,

            }).ToList();
            try
            {
                var result = DBHelper.Context.Insert(datamodel);
                processresult.Add("一级分类数插入", ProcessResultType.成功, result.ToString());

            }
            catch (Exception e)
            {
                processresult.Add("一级分类数插入", ProcessResultType.失败, e.Message);
                return processresult;
            }

            var topClassCookClassify = DBHelper.Context.From<cook_classify>().Where(c => c.cook_class == 0).ToList();
            if (topClassCookClassify != null && topClassCookClassify.Count > 0)
            {
                processresult.Add("二级分类读取", ProcessResultType.成功, topClassCookClassify.Count.ToString());

                foreach (var item in topClassCookClassify)
                {
                    CookClassifyEntity subdata = null;
                    try
                    {
                        subdata = await recipeServer.GetCookClassify(item.Id.ToString());

                    }
                    catch (Exception e)
                    {
                        processresult.Add("二级分类获取或解析", ProcessResultType.失败, e.Message);


                    }
                    if (subdata != null && subdata.Status)
                    {
                        processresult.Add("二级分类数据验证", ProcessResultType.成功);

                    }
                    else
                    {
                        processresult.Add("二级分类数据验证", ProcessResultType.失败);

                        continue;
                    }
                    var subdatamodel = subdata.Tngou.Select(c => new cook_classify()
                    {
                        Id = c.Id,
                        cook_class = c.Cookclass,
                        description = c.Description,
                        name = c.Name,
                        seq = 1,
                        title = c.Title,

                    }).ToList();
                    try
                    {
                        var result = DBHelper.Context.Insert(subdatamodel);
                        processresult.Add("二级分类数插入", ProcessResultType.成功, result.ToString(), new string[] { "当前分类" + item.Id });

                    }
                    catch (Exception e)
                    {
                        processresult.Add("二级分类数插入", ProcessResultType.失败, e.Message, new string[] { "当前分类" + item.Id });
                        continue;
                    }
                }

            }
            else
            {
                processresult.Add("二级分类读取", ProcessResultType.失败, "没找到二级分类数据");

            }
            return processresult;
        }

        /// <summary>
        /// 获取菜谱列表
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessResult> GetCookShowItem()
        {

            var processresult = new ProcessResult();


            var clearresult = DBHelper.Context.DeleteAll<cook_show_item>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "cook_show_item" });
            int flag = 0;
            for (int i = 1; i < int.MaxValue; i++)
            {

                CookListEntity data = null;
                try
                {
                    data = await recipeServer.GetCookListByPage(i.ToString());

                }
                catch (Exception e)
                {
                    processresult.Add("菜谱列表获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前页" + i });
                    continue;
                }
                if (data != null && data.Status)
                {
                    if (data.Tngou.Count == 0)
                    {
                        processresult.Add("菜谱列表数据验证", ProcessResultType.成功, "无数据表明已经结束", new string[] { "当前页" + i });
                        if (flag > 5)
                        {
                            break;
                        }
                        flag++;
                        continue;
                    }
                    processresult.Add("菜谱列表数据验证", ProcessResultType.成功, data.Tngou.Count().ToString(), new string[] { "当前页" + i });

                }
                else
                {
                    processresult.Add("菜谱列表数据验证", ProcessResultType.失败, "返回数据状态为Flase", new string[] { "当前页" + i });

                    continue;
                }
                var datamodel = data.Tngou.Select(c => new cook_show_item()
                {
                    Id = c.Id,
                    count = c.Count,
                    //description = c.Description,
                    fcount = c.Fcount,
                    food = GetShortStr(c.Food, 299),
                    img = c.Img,
                    name = c.Name,
                    rcount = c.Rcount

                }).ToList();
                try
                {
                    var result = DBHelper.Context.Insert(datamodel);
                    processresult.Add("菜谱数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前页" + i });

                }
                catch (Exception e)
                {
                    processresult.Add("菜谱数据插入", ProcessResultType.失败, e.Message, param: new string[] { "当前页" + i });
                    continue;
                }
            }



            return processresult;
        }

        /// <summary>
        /// 获取各个菜谱的分类并更新列表
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessResult> GetShowItemClassify()
        {

            var processresult = new ProcessResult();
            var clearresult = DBHelper.Context.DeleteAll<cook_2_cook_classify>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "show_item_classify" });

            var subClassify = DBHelper.Context.From<cook_classify>().Where(c => c.seq == 1).ToList();

            foreach (var item in subClassify)
            {
                for (int i = 1; i < int.MaxValue; i++)
                {

                    CookListEntity data = null;
                    try
                    {
                        data = await recipeServer.GetCookListById(i.ToString(), item.Id.ToString());

                    }
                    catch (Exception e)
                    {
                        processresult.Add("菜谱列表获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前子类" + item.Id, "当前页" + i });
                        continue;
                    }

                    if (data != null && data.Status)
                    {
                        if (data.Tngou.Count == 0)
                        {
                            processresult.Add("菜谱列表数据验证", ProcessResultType.成功, "无数据表明已经结束", new string[] { "当前子类" + item.Id, "当前页" + i });

                            break;
                        }
                        processresult.Add("菜谱列表数据验证", ProcessResultType.成功, data.Tngou.Count().ToString(), new string[] { "当前子类" + item.Id, "当前页" + i });

                    }
                    else
                    {
                        processresult.Add("菜谱列表数据验证", ProcessResultType.失败, "返回数据状态为Flase", new string[] { "当前子类" + item.Id, "当前页" + i });

                        continue;
                    }
                    var datamodel = data.Tngou.Select(c => new cook_2_cook_classify()
                    {
                        item_id = c.Id,
                        classify_id = item.Id

                    }).ToList();

                    try
                    {
                        var result = DBHelper.Context.Insert(datamodel);
                        processresult.Add("菜谱类型数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前子类" + item.Id, "当前页" + i });

                    }
                    catch (Exception e)
                    {
                        processresult.Add("菜谱类型数据插入", ProcessResultType.失败, e.Message, new string[] { "当前子类" + item.Id, "当前页" + i });
                        continue;
                    }


                }

            }

            return processresult;
        }

        /// <summary>
        /// 补充获取菜谱列表
        /// </summary>
        /// <param name="reprocessPageIndex"></param>
        /// <returns></returns>
        private async Task<ProcessResult> ReGetCookList(List<string> reprocessPageIndex)
        {
            var processresult = new ProcessResult();
            foreach (var i in reprocessPageIndex)
            {
                CookListEntity data = null;
                try
                {
                    data = await recipeServer.GetCookListByPage(i.ToString());

                }
                catch (Exception e)
                {
                    processresult.Add("补取菜谱列表获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前页" + i });
                    continue;
                }
                if (data != null && data.Status)
                {

                    processresult.Add("补取菜谱列表数据验证", ProcessResultType.成功, data.Tngou.Count().ToString(), new string[] { "当前页" + i });

                }
                else
                {
                    processresult.Add("补取菜谱列表数据验证", ProcessResultType.失败, "返回数据状态为Flase", new string[] { "当前页" + i });

                    continue;
                }
                var datamodel = data.Tngou.Select(c => new cook_show_item()
                {
                    Id = c.Id,
                    count = c.Count,
                    //description = c.Description,
                    fcount = c.Fcount,
                    food = GetShortStr(c.Food, 299),
                    img = c.Img,
                    name = c.Name,
                    rcount = c.Rcount

                }).ToList();

                foreach (var cookShowItem in datamodel)
                {
                    try
                    {
                        var result = DBHelper.Context.Insert(cookShowItem);
                        processresult.Add("菜谱数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前页" + i, "当前ID" + cookShowItem.Id });

                    }
                    catch (Exception e)
                    {
                        processresult.Add("菜谱数据插入", ProcessResultType.失败, e.Message, param: new string[] { "当前页" + i, "当前ID" + cookShowItem.Id });
                        continue;
                    }

                }



            }
            return processresult;
        }

        /// <summary>
        /// 获取菜谱详细
        /// </summary>
        /// <returns></returns>
        private async Task<ProcessResult> GetCookDetail()
        {
            var processresult = new ProcessResult();

            var clearresult = DBHelper.Context.DeleteAll<cook_detail>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "cook_detail" });


            List<int> data = new List<int>();
            try
            {
                data = DBHelper.Context.From<cook_show_item>().Select(c => c.Id).ToList<int>();

            }
            catch (Exception e)
            {
                processresult.Add("菜谱列表获取或解析", ProcessResultType.失败, e.Message);
            }

            foreach (var item in data)
            {
                CookDetailEntity c = null;
                try
                {
                    c = await recipeServer.GetCookDetail(item.ToString());

                }
                catch (Exception e)
                {
                    processresult.Add("菜谱详情获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前页" + item });
                    continue;
                }
                if (c == null || c.Status == false)
                {
                    processresult.Add("菜谱详情数据验证", ProcessResultType.失败, null, new string[] { "当前页" + item });
                    continue;
                }
                var datamodel = new cook_detail()
                {
                    cook_id = c.Id,
                    count = c.Count,
                    //description = c.Description,
                    fcount = c.Fcount,
                    food = GetShortStr(c.Food, 299),
                    img = c.Img,
                    message = c.Message,
                    name = c.Name,
                    rcount = c.Rcount

                };

                try
                {
                    var result = DBHelper.Context.Insert(datamodel);
                    processresult.Add("菜谱详情数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前ID" + item });

                }
                catch (Exception e)
                {
                    processresult.Add("菜谱详情数据插入", ProcessResultType.失败, e.Message, param: new string[] { "当前ID" + item });
                }
            }

            return new ProcessResult();

        }

        /// <summary>
        /// 获取食物列表
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessResult> GetFoodShowItem()
        {

            var processresult = new ProcessResult();


            var clearresult = DBHelper.Context.DeleteAll<food_show_item>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "food_show_item" });
            int flag = 0;
            for (int i = 1; i < int.MaxValue; i++)
            {

                FoodListEntity data = null;
                try
                {
                    data = await recipeServer.GetFoodListByPage(i.ToString());

                }
                catch (Exception e)
                {
                    processresult.Add("食物列表获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前页" + i });
                    continue;
                }
                if (data != null && data.Status)
                {
                    if (data.Tngou.Count == 0)
                    {
                        processresult.Add("食物列表数据验证", ProcessResultType.成功, "无数据表明已经结束", new string[] { "当前页" + i });
                        if (flag > 5)
                        {
                            break;
                        }
                        flag++;
                        continue;
                    }
                    processresult.Add("食物列表数据验证", ProcessResultType.成功, data.Tngou.Count().ToString(), new string[] { "当前页" + i });

                }
                else
                {
                    processresult.Add("食物列表数据验证", ProcessResultType.失败, "返回数据状态为Flase", new string[] { "当前页" + i });

                    continue;
                }
                var datamodel = data.Tngou.Select(c => new food_show_item()
                {
                    Id = c.Id,
                    count = c.Count,
                    description = c.Description,
                    disease = GetShortStr(c.Disease, 299),
                    fcount = c.Fcount,
                    food = c.Food,
                    img = c.Img,
                    name = c.Name,
                    rcount = c.Rcount,
                    summary = c.Summary,

                }).ToList();
                try
                {
                    var result = DBHelper.Context.Insert(datamodel);
                    processresult.Add("食物数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前页" + i });

                }
                catch (Exception e)
                {
                    processresult.Add("食物数据插入", ProcessResultType.失败, e.Message, param: new string[] { "当前页" + i });
                    continue;
                }
            }



            return processresult;
        }


        /// <summary>
        /// 获取菜谱详细
        /// </summary>
        /// <returns></returns>
        private async Task<ProcessResult> GetFoodDetail()
        {
            var processresult = new ProcessResult();

            var clearresult = DBHelper.Context.DeleteAll<food_detail>();
            processresult.Add(ProcessResult.CleanDB, ProcessResultType.成功, clearresult.ToString(), new[] { "food_detail" });


            List<int> data = new List<int>();
            try
            {
                data = DBHelper.Context.From<food_show_item>().Select(c => c.Id).ToList<int>();

            }
            catch (Exception e)
            {
                processresult.Add("食物列表获取或解析", ProcessResultType.失败, e.Message);
            }

            foreach (var item in data)
            {
                FoodDetailEntity c = null;
                try
                {
                    c = await recipeServer.GetFoodDetail(item.ToString());

                }
                catch (Exception e)
                {
                    processresult.Add("食物详情获取或解析", ProcessResultType.失败, e.Message, new string[] { "当前页" + item });
                    continue;
                }
                if (c == null || c.Status == false)
                {
                    processresult.Add("食物详情数据验证", ProcessResultType.失败, null, new string[] { "当前页" + item });
                    continue;
                }

                var datamodel = new food_detail()
                {
                    food_id = c.Id,
                    count = c.Count,
                    description = c.Description,
                    fcount = c.Fcount,
                    food = GetShortStr(c.Food, 299),
                    img = c.Img,
                    message = c.Message,
                    name = c.Name,
                    rcount = c.Rcount,
                    status = c.Status,
                    summary = c.Summary,


                };

                try
                {
                    var result = DBHelper.Context.Insert(datamodel);
                    processresult.Add("食物详情数据插入", ProcessResultType.成功, result.ToString(), new string[] { "当前ID" + item });

                }
                catch (Exception e)
                {
                    processresult.Add("食物详情数据插入", ProcessResultType.失败, e.Message, param: new string[] { "当前ID" + item });
                }
            }

            return new ProcessResult();

        }


        private string GetShortStr(string source, int maxlength)
        {
            char sep = ',';
            string result = "";
            if (source.Length > maxlength)
            {
                var handledstr = source.Substring(0, maxlength);
                var tempArry = handledstr.Split(sep).ToList();
                tempArry.RemoveAt(tempArry.Count - 1);
                result = GetShortStr(string.Join(sep.ToString(), tempArry), maxlength);

            }
            else
            {
                result = source;
            }


            return result;
        }

    }
}
