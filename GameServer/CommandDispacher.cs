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

            Thread tr = new Thread(new ThreadStart(StartDispacher));
            tr.Start();
        }

        private void StartDispacher()
        {
            while (true)
            {
                for (int i = 0; i < clients.clientsList.Count; i++)
                {
                    if (clients.clientsList[i].netStream.DataAvailable)
                    {
                        string message = clients.clientsList[i].Read();

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
                                auth.Dispacher(clients.clientsList[i], info);
                                break;
                            case "Lobby":
                                Lobby lobby = new Lobby();
                                lobby.Dispacher(clients.clientsList[i], info, clients.clientsList);
                                break;
                            case "HandShake":
                                HandShake handShake = new HandShake(clients, rooms);
                                handShake.Dispacher(clients.clientsList[i], info);
                                break;
                            case "Game":
                                game.Dispacher(clients.clientsList[i], info);
                                break;
                        }
                    }
                }
            }
        }

    }
}
