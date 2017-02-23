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
        Info info;

        public EventHandler LogIn;

        public Authorization(Client client)
        {
            this.client = client;
            info = new Info();
            info.Module = "Auth";
        }

        public void Dispacher(Info tmpinfo)
        {
            switch (info.Command)
            {
                case "LogIn":
                    LogIn(tmpinfo.Message, null);
                    break;
            }
        }

        public void SendRegistration(string name, string password)
        {
            info.Command = "Registration";
            info.Message.Clear();
            info.Message.Add(name);
            info.Message.Add(password);
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }

        public void SendLogIn(string name, string password)
        {
            info.Command = "LogIn";
            info.Message.Clear();
            info.Message.Add(name);
            info.Message.Add(password);
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
        public void SendLogout(object sender, EventArgs e)
        {
            info.Command = "LogOut";
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
