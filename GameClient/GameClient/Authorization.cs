using GameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Authorization
    {
        public Client client;
        RequestObject info;
        
        public EventHandler LogIn;

        public Authorization(Client client)
        {
            this.client = client;
            info = new RequestObject();
            info.Module = "Auth";
        }

        public void Dispacher(RequestObject tmpinfo)
        {
            switch (info.Cmd)
            {
                case "LogIn":
                    LogIn(tmpinfo.Args.ToString(), null);
                    break;
            }
        }

        public void SendRegistration(string name, string password)
        {
            info.Cmd = "Registration";
            info.Args = new object[] { name, password };
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }

        public void SendLogIn(string name, string password)
        {
            info.Cmd = "LogIn";
            info.Args = new object[] { name, password };
        
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
        public void SendLogout(object sender, EventArgs e)
        {
            info.Cmd = "LogOut";
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
