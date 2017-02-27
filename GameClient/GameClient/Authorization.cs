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
        public void senderer(RequestObject info)
        {
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(JsonConvert.SerializeObject(info));
            writer.Flush();
        }
        public void SendRegistration(string name, string password)
        {
            senderer(new RequestObject("Auth", "Registration", new object[] { name, password }));
        }

        public void SendLogIn(string name, string password)
        {
            senderer(new RequestObject("Auth", "LogIn", new object[] { name, password }));
        }
        public void SendLogout(object sender, EventArgs e)
        {
            senderer(new RequestObject("Auth", "LogOut", null));
        }
    }
}
