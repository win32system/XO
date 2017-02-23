using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Lobby
    {
        public void Dispacher(Client client, Info info, List<Client> clientsList)
        {
            switch (info.Command)
            {
                case "refreshClients":
                    SendClients(client, clientsList);
                    break;
            }
        }
        public void SendClients(Client client, List<Client> clientsList)
        {
            Info info = new Info();
            info.Module = "Lobby";
            info.Command = "refreshClients";
            info.Message.Clear();
            foreach (var item in clientsList)
            {
                if (item == client || item.name == ""||item.inGame)
                    continue;
                info.Message.Add(item.name);
            }
            string strInfo = JsonConvert.SerializeObject(info);
            client.Write(strInfo);
        }
        public void SendNotification(string notification, Client client)
        {
            Info info = new Info();
            info.Module = "Lobby";
            info.Command = "Notification";
            info.Message.Add(notification);
            string strInfo = JsonConvert.SerializeObject(info);
            client.Write(strInfo);
        }
    }
}
