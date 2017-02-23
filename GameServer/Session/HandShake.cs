using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class HandShake
    {
        Clients clients;
        Rooms rooms;
        Info info;

        public HandShake(Clients clients, Rooms rooms)
        {
            this.clients = clients;
            this.rooms = rooms;
            info = new Info();
            info.Module = "HandShake";
        }

        public void Dispacher(Client client, Info info)
        {
            switch (info.Command)
            {
                case "Invite":
                    Invite(client, info.Message[0], info.Message[1]);
                    break;
                case "Ok":
                    Start(client, info.Message[0], info.Message[1]);
                    break;
                case "Cancle":
                    Cancle(client, info.Message[0]);
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
            if(clientinvited==null)
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
            info.Command = "Invited";
            info.Message.Clear();
            info.Message.Add(clientcreator.name);
            info.Message.Add(gameName);
            string strInfo = JsonConvert.SerializeObject(info);
            clientinvited.Write(strInfo);

            info.Command = "Wait";
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
                info.Command = "Start";
                info.Message.Clear();
                info.Message.Add("" + (rooms.rooms.Count-1));
                info.Message.Add(gameName);
                string strInfo = JsonConvert.SerializeObject(info);
                tmpclients[i].Write(strInfo);
            }
        }
        private void Cancle(Client invitedClient, string creatorName)
        {
            Client creator = clients.clientsList.Find(c => c.name == creatorName);

            creator.isBusy = false;
            invitedClient.isBusy = false;

            info.Command = "Cancle";
            string strInfo = JsonConvert.SerializeObject(info);
            creator.Write(strInfo);
        }
    }
}
