using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM;

namespace MatoRecipe_Server.Helper
{
    public class DBHelper
    {
        public static readonly DbSession Context = new DbSession("DosConnLocal");
    }
}