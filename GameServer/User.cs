using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class User
    {
        public string name;
        public string password;
        public int type;
        public User(string name, string password /*, int type*/)
        {
            this.name = name;
            this.password = password;
            //this.type = type; 
        }
    }
}
