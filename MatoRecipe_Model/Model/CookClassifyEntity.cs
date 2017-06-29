﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System.Collections.Generic;
using Newtonsoft.Json;

namespace MatoRecipe_Model.Model
{

    public class CookClassify
    {

        [JsonProperty("cookclass")]
        public int Cookclass { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("seq")]
        public int Seq { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class CookClassifyEntity
    {

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("tngou")]
        public List<CookClassify> Tngou { get; set; }
    }

}
