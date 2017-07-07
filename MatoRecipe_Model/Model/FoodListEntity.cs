using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MatoRecipe_Model.Model
{
    public class Tngou
    {

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("disease")]
        public string Disease { get; set; }

        [JsonProperty("fcount")]
        public int Fcount { get; set; }

        [JsonProperty("food")]
        public string Food { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rcount")]
        public int Rcount { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("symptom")]
        public string Symptom { get; set; }
    }

    public class FoodListEntity
    {

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("tngou")]
        public List<Tngou> Tngou { get; set; }
    }

}
