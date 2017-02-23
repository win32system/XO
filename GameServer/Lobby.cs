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
            RequestObject info = new RequestObject("Lobby", "refreshClients",null);
            foreach (var item in clientsList)
            {
                if (item == client || item.name == ""||item.inGame)
                    continue;
                info.Args = item.name;
            }
            string strInfo = JsonConvert.SerializeObject(info);
            client.Write(strInfo);
        }
        public void SendNotification(string notification, Client client)
        {
            RequestObject info = new RequestObject("Lobby", "Notification", notification);
            string strInfo = JsonConvert.SerializeObject(info);
            client.Write(strInfo);
        }
    }
}
