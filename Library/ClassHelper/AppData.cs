using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ClassHelper
{
    internal class AppData
    {
        public static DB.LibraryEntities3 Context { get; } = new DB.LibraryEntities3();
    }
}
