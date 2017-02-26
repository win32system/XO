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
        public string email;

        public User(string name, string password, string email)
        {
            this.name = name;
            this.password = password;
            this.email = email;
        }
    }
}
