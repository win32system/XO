using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public static class GameCreator
    {
        public static IGame CreateInstance(string type)
        {
            IGame result = null;
            switch (type)
            {
                case "XO":
                    result = new XO();
                    break;

            }
            return result;
        }
    }
}
