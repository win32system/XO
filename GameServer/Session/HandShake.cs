using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class HandShake
    {
        private Clients clients { get; set; }
        Rooms rooms { get; set; }
        RequestObject info { get; set; }

        public HandShake(Clients clients, Rooms rooms)
        {
            this.clients = clients;
            this.rooms = rooms;
            info = new RequestObject();
            info.Module = "HandShake";
        }

        public void Dispacher(Client client, RequestObject info)
        {
            object []arg = JsonConvert.DeserializeObject<object[]>(info.Args.ToString());
            switch (info.Cmd)
            {
                case "Invite":
                    if (arg[0] != null && arg[1] != null)
                    {
                        Invite(client, arg[0].ToString(), arg[1].ToString());
                    }
                    break;
                case "Ok":
                    Start(client, arg[0].ToString(), arg[1].ToString());
                    break;
                case "Cancle":
                    Cancle(client, arg[0].ToString());
                    break;
            }
        }

        private void Invite(Client clientcreator, string invitedName, string gameName)
        {
            clientcreator.isBusy = true;
            Client clientinvited = clients.clientsList.Find(c => c.name == invitedName);
            
            if(clientinvited.inGame)
            {
                Lobby lobby = new Lobby();
                lobby.SendNotification("Данный пользователь уже находится в игре", clientinvited);
                lobby.SendClients(clientinvited, clients.clientsList);
                return;
            }
            if (clientinvited==null)
            {
                Lobby lobby = new Lobby();
                lobby.SendNotification("Данный пользователь уже вышел из системы", clientinvited);
                lobby.SendClients(clientinvited, clients.clientsList);
                return;
            }
            if(clientinvited.isBusy)
            {
                Lobby lobby = new Lobby();
                lobby.SendNotification("Данный пользователь сейчас занят", clientcreator);
                clientcreator.isBusy = false;
                return;
            }
            clientinvited.isBusy = true;
            info.Cmd = "Invited";
            info.Args = new object[] { clientcreator.name, gameName };
            
            string strInfo = JsonConvert.SerializeObject(info);
            clientinvited.Write(strInfo);

            info.Cmd = "Wait";
            strInfo = JsonConvert.SerializeObject(info);
            clientcreator.Write(strInfo);
        }

        private void Start(Client invitedClient, string creatorName, string gameName)
        {
            Client creatorClient = this.clients.clientsList.Find(c=> c.name == creatorName);
            List<Client> tmpclients = new List<Client>();
            tmpclients.Add(creatorClient);
            tmpclients.Add(invitedClient);

            rooms.Add(tmpclients, gameName);

            for(int i=0; i<tmpclients.Count; i++)
            {
                info.Module = "Game";
                info.Cmd = "Start";
                info.Args = new object[] {rooms.rooms.Count - 1, gameName };
             
                string strInfo = JsonConvert.SerializeObject(info);
                tmpclients[i].Write(strInfo);
            }
        }
        private void Cancle(Client invitedClient, string creatorName)
        {
            Client creator = clients.clientsList.Find(c => c.name == creatorName);

            creator.isBusy = false;
            invitedClient.isBusy = false;

            info.Cmd = "Cancle";
            string strInfo = JsonConvert.SerializeObject(info);
            creator.Write(strInfo);
        }
    }
}
