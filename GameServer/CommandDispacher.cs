using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class CommandDispacher
    {
        private Clients clients { get; set; }
        Rooms rooms { get; set; }
        Game game { get; set; }

        public CommandDispacher(Clients clients)
        {
            this.clients = clients;
            rooms = new Rooms();
            game = new Game(rooms);

            Server.tr = new Thread(new ThreadStart(StartDispacher));
            Server.tr.Start();


        }
        private void writeClient(Client client)
        {
            if (client.netStream.DataAvailable)
            {
                string message = client.Read();

                RequestObject info = new RequestObject();
                try
                {
                    info = JsonConvert.DeserializeObject<RequestObject>(message);
                }
                catch (Exception)
                {

                }
                switch (info.Module)
                {
                    case "Auth":
                        Authorization auth = new Authorization(clients);
                        auth.Dispacher(client, info);
                        break;
                    case "Lobby":
                        Lobby lobby = new Lobby();
                        lobby.Dispacher(client, info, clients.clientsList);
                        break;
                    case "HandShake":
                        HandShake handShake = new HandShake(clients, rooms);
                        handShake.Dispacher(client, info);
                        break;
                    case "Game":
                        game.Dispacher(client, info);
                        break;
                }
            }
        }
        private void StartDispacher()
        {
            while (Server.go)
            {

                for (int i = 0; i < clients.clientsList.Count; i++)
                {
                    writeClient(clients.clientsList[i]);
                }
            }
        }
    }
}

 