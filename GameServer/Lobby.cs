using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Lobby
    {
        public void Dispacher(Client client, RequestObject info, List<Client> clientsList)
        {
            switch (info.Cmd)
            {
                case "refreshClients":
                    SendClients(client, clientsList);
                    break;
            }
        }
        public void SendClients(Client client, List<Client> clientsList)
        {
            List<string> str = new List<string>();
            foreach (var item in clientsList)
            {
                if (item == client || item.name == "" || item.inGame==true || item.name==null)
                    continue;
                 str.Add(item.name);
            }
            client.Write(JsonConvert.SerializeObject(new RequestObject("Lobby", "refreshClients", str)));
        }
        public void SendNotification(string notification, Client client)
        {
            RequestObject info = new RequestObject("Lobby", "Notification", notification);
            client.Write(JsonConvert.SerializeObject(info));
        }
    }
}
