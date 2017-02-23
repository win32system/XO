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
    class Lobby
    {
        Client client;
        RequestObject info;
        public Lobby(Client client)
        {
            this.client = client;
            info = new RequestObject();
            info.Module = "Lobby";
        }
        
        public EventHandler RefreshClients;

        public EventHandler Notification;

        public void Dispacher(RequestObject tmpinfo)
        {
            switch (tmpinfo.Cmd)
            {
                case "refreshClients":
                    RefreshClients(tmpinfo.Args, null);
                    break;
                case "Notification":
                    Notification(tmpinfo.Args, null);
                    break;
            }
        }

        public void SendRefreshClients(object sender, EventArgs e)
        {
            info.Cmd = "refreshClients";
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
