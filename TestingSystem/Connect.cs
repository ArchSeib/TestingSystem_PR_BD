using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem
{
    internal class Connect
    {
        private static TestingSystemEntities bd;
        public static TestingSystemEntities GetContext()
        {
            if (bd == null)
            {
                bd = new TestingSystemEntities();
            }
            return bd;
        }
    }
}
