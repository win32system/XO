using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Room
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
                    LogProvider.AppendRecord(string.Format("{0}  user [{1}] - X/0 ", clients[0].name));
                    sendMessage(JsonConvert.SerializeObject(new RequestObject("Game", "Role", clients[0].name )));
                break;
            }
        }
        public void sendMessage(string messageToSend)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Write(messageToSend);
            }
        }
        public void Move(string senderName, object message)
        {
            if (game.IsTurn(senderName))
            {
                string messageToSend = game.Move(message.ToString());
                if(messageToSend!=null)
                {
                    sendMessage(messageToSend);
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
                    LogProvider.AppendRecord(string.Format("{0}  Game Over [{1}]", clients[0].name));
                    string strInfo = JsonConvert.SerializeObject(new RequestObject("Game","Over", null));
                    clients[i].Write(strInfo);
                }
                return true;
            }
            return false;
        }
    }
}
