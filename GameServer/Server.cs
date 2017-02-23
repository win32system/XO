using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Server
    {
        TcpListener server;
        Clients clients;
      
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            server.Start();
            clients = new Clients();
            
            CommandDispacher dispacher = new CommandDispacher(clients);
        }

        public void Start()
        {
            while(true)
            {
                clients.Add(server);
            }
        }
    }
}
