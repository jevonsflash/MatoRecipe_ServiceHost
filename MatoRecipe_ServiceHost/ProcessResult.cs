using System;
using System.Collections.Generic;
using System.Text;
using MatoRecipe_Generator.Session;

namespace MatoRecipe_Generator
{
    public class ProcessResult
    {

        public static readonly string DB = "数据库操作";
        public static readonly string Get = "Get请求";

        public static readonly string Succ = "成功";
        public static readonly string Err = "失败";
        public ProcessResult()
        {
            Log = new List<ProcessResultItem>();
        }
        public List<ProcessResultItem> Log { get; set; }
        public string ExtMsg { get; set; }
        public bool IsSuccess { get; set; }

        public void Add(ProcessResultItem item)
        {
            this.Log.Add(item);
            Console.WriteLine(string.Format("{0} \t {1} \t 更改数量{2}", item.Title, item.Content, item.Count));
            LogSession.Log.Add(item);
        }

        public string Show()
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in Log)
            {
                result.AppendLine(string.Format("{0} \t {1} \t 更改数量{2}", item.Title, item.Content, item.Count));
            }
            return result.ToString();
        }
    }

    public class ProcessResultItem
    {
        public ProcessResultItem(int count, string title, string content)
        {
            this.Count = count;
            this.Content = content;
            this.Title = title;

        }

        public string Title { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }
    }

}