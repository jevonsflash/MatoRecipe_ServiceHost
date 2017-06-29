using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MatoRecipe.Helper;
using MatoRecipe_Generator.Helper;
using MatoRecipe_Model.Model;
using Newtonsoft.Json;

namespace MatoRecipe_Generator.Server
{
    public class RecipeServer
    {
        private static readonly HttpHelper HttpHelper = new HttpHelper();

        public async Task<CookListEntity> GetCookSearch(string parameter)
        {
            var result = new CookListEntity();
            string url = StaticURLHelper.CookByName;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", parameter);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<CookListEntity>(jsonstr);
            return result;
        }

        public async Task<CookListEntity> GetCookList(string parameter)
        {
            var result = new CookListEntity();
            string url = StaticURLHelper.CookList;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", parameter);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<CookListEntity>(jsonstr);
            return result;
        }

        public async Task<CookDetailEntity> GetCookDetail(string parameter)
        {
            var result = new CookDetailEntity();
            string url = StaticURLHelper.CookShow;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", parameter);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<CookDetailEntity>(jsonstr);
            return result;
        }


        public async Task<CookClassifyEntity> GetCookClassify()
        {
            var result = new CookClassifyEntity();
            string url = StaticURLHelper.CookClassify;
            var jsonstr = await GetJSON(url, null);
            result = JsonConvert.DeserializeObject<CookClassifyEntity>(jsonstr);
            return result;
        }



        public async Task<FoodDetailEntity> GetFoodSearch(string parameter)
        {
            var result = new FoodDetailEntity();
            string url = StaticURLHelper.FoodByName;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", parameter);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<FoodDetailEntity>(jsonstr);
            return result;
        }

        private async Task<string> GetJSON(string url, Dictionary<string, string> parameters)
        {
            string postString = url;
            if (parameters != null && parameters.Count > 0)
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                postString = postString + "?" + buffer;

            }
            string resposeString = await HttpHelper.GetUrlResposeAsnyc(postString).ConfigureAwait(false);

            return resposeString;
        }
    }
}