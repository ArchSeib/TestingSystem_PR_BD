using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem
{
    internal class Connect
    {
        private static TestingSystemEntities0 bd;
        public static TestingSystemEntities0 GetContext()
        {
            if (bd == null)
            {
                bd = new TestingSystemEntities0();
            }
            return bd;
        }
    }
}
