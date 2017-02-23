using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Room
    {
        IGame game;
        public List<Client> clients;
        

        public Room(List<Client> clients, string gameName)
        {
            this.clients = clients;
            switch(gameName)
            {
                case "XO":
                    game = new XO(clients[0].name, clients[1].name);
                    clients[0].inGame = true;
                    clients[1].inGame = true;
                    break;
            }
        }

        public void Move(string senderName, List<string> message)
        {
            if (game.IsTurn(senderName))
            {
                string messageToSend = game.Move(message);
                if(messageToSend!=null)
                {
                    for(int i=0; i<clients.Count; i++)
                    {
                        clients[i].Write(messageToSend);
                    }
                }
            }
        }
        public bool IsOver()
        {
            if (game.IsOver())
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].inGame = false;
                    clients[i].isBusy = false;
                    Info info = new Info();
                    info.Module = "Game";
                    info.Command = "Over";
                    string strInfo = JsonConvert.SerializeObject(info);
                    clients[i].Write(strInfo);
                }
                return true;
            }
            return false;
        }
    }
}
