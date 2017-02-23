using GameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    class Listener
    {
        Client client;
        Authorization auth;
        Lobby lobby;
        HandShake handShake;
        Game game;

        public Listener(Client client, Authorization auth, Lobby lobby, HandShake handShake, Game game)
        {
            this.client = client;
            this.auth = auth;
            this.lobby = lobby;
            this.handShake = handShake;
            this.game = game;

            Thread listenthread = new Thread(new ThreadStart(Listen));
            listenthread.Start();
        }

        private void Listen()
        {
            while(true)
            {
                StreamReader reader = new StreamReader(client.netstream);
                string message = reader.ReadLine();

                RequestObject info = JsonConvert.DeserializeObject<RequestObject>(message);
                switch (info.Module)
                {
                    case "Auth":
                        auth.Dispacher(info);
                        break;
                    case "Lobby":
                        lobby.Dispacher(info);
                        break;
                    case "HandShake":
                        handShake.Dispacher(info);
                        break;
                    case "Game":
                        game.Dispacher(info);
                        break;
                }
            }
        }
    }
}
