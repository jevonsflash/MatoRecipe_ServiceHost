using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MatoRecipe_Generator.Session;

namespace MatoRecipe_Generator
{
    public class ProcessResult
    {

        public static readonly string DB = "数据库操作";
        public static readonly string Data = "数据获取操作";
        public static readonly string CleanDB = "清空数据库";

        public static readonly string Succ = "成功";
        public static readonly string Err = "失败";
        public ProcessResult()
        {
        }
        public object ExtMsg { get; set; }
        public bool IsSuccess { get; set; }

        public void Add(ProcessResultItem item)
        {
            Console.WriteLine(Format(item));
            Record(item);
        }

        private void Record(ProcessResultItem item)
        {
            LogSession.Log.Add(item);
            try
            {

                var path = string.Format("D:\\MainLog{0}.{1}", DateTime.Now.ToString("yy-MM-dd"), "txt");
                File.AppendAllLines(path,
                    new List<string>() { Format(item) });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void Add(string content, ProcessResultType result, string response = "NullorEmpty", string[] param = null)
        {
            var item = new ProcessResultItem(response, param ?? new string[] { "没有参数" }, content, result);
            Console.WriteLine(Format(item));
            Record(item);
        }
        private string Format(ProcessResultItem item)
        {
            return string.Format("活动：{0} \t 参数：{1} \t 返回值：{2} \t 结果：{3} \t 时间：{4}", item.Content, GetStr(item.Param), item.Response, item.Result, DateTime.Now.ToString("u"));
        }

        private string GetStr(string[] param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item
                in param)
            {
                sb.Append(item);
                sb.Append("\t");
            }
            return sb.ToString();
        }
    }


    public class ProcessResultItem
    {
        public ProcessResultItem(string response, string[] param, string content, ProcessResultType result)
        {
            this.Response = response;
            this.Content = content;
            this.Param = param;
            this.Result = result;

        }

        public string[] Param { get; set; }
        public string Content { get; set; }
        public string Response { get; set; }
        public ProcessResultType Result { get; set; }
    }

    public enum ProcessResultType
    {
        成功, 失败
    }

}