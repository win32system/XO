using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Lobby
    {
        Client client;
        Info info;
        public Lobby(Client client)
        {
            this.client = client;
            info = new Info();
            info.Module = "Lobby";
        }
        
        public EventHandler RefreshClients;

        public EventHandler Notification;

        public void Dispacher(Info tmpinfo)
        {
            switch (tmpinfo.Command)
            {
                case "refreshClients":
                    RefreshClients(tmpinfo.Message, null);
                    break;
                case "Notification":
                    Notification(tmpinfo.Message, null);
                    break;
            }
        }

        public void SendRefreshClients(object sender, EventArgs e)
        {
            info.Command = "refreshClients";
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
