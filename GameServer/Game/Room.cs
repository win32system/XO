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
            game = GameCreator.CreateInstance(gameName);
            if (game == null)
                return;

            this.clients = clients;

            clients[0].inGame = true;
            clients[1].inGame = true;
             
        }
    

        public void Move(string senderName, object message)
        {
            if (game.IsGameOver Turn(senderName))
            {
                string messageToSend = game.Move(message.ToString());
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
            if (game.IsGameOver())
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].inGame = false;
                    clients[i].isBusy = false;

                    clients[i].Write(JsonConvert.SerializeObject(new RequestObject("Game", "Over", null)));
                }
                return true;
            }
            return false;
        }
    }
}
/*
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

        public void Move(string senderName, object message)
        {
            if (game.IsTurn(senderName))
            {
                string messageToSend = game.Move(message.ToString());
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
                    RequestObject info = new RequestObject();
                    info.Module = "Game";
                    info.Cmd = "Over";
                    string strInfo = JsonConvert.SerializeObject(info);
                    clients[i].Write(strInfo);
                }
                return true;
            }
            return false;
        }

*/
