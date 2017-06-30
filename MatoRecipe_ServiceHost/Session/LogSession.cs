using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatoRecipe_Generator.Session
{
    public static class LogSession
    {
        static LogSession()
        {
            Log=new List<ProcessResultItem>();
        }
        public static List<ProcessResultItem> Log;
    }
}
