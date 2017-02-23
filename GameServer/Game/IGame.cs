using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    interface IGame
    {
        bool IsTurn(string name);
        string Move(List<string> message);
        bool IsOver();
    }
}
