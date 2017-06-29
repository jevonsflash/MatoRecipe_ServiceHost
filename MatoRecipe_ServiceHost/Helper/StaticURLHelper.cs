namespace MatoRecipe.Helper
{
    public static class StaticURLHelper
    {
        //基础 www.tngou.net/api/cook/classify 取得菜谱分类，可以通过分类id取得问答列表
        //重要  www.tngou.net/api/cook/list 取得菜谱列表，也可以用分类id作为参数
        //一般  www.tngou.net/api/cook/name 取得菜谱名称详情，通过name取得菜谱详情
        //重要  www.tngou.net/api/cook/show 取得菜谱信息，菜谱id取得该对应详细内容信息
        public static string CookClassify = "http://www.tngou.net/api/cook/classify";
        public static string CookList = "http://www.tngou.net/api/cook/list";
        public static string CookByName = "http://www.tngou.net/api/cook/name";
        public static string CookShow = "http://www.tngou.net/api/cook/show";

        public static string FoodByName = "http://www.tngou.net/api/food/name";

    }
}
