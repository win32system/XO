﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class Client
    {
        public TcpClient client;
        public NetworkStream netstream;

        public Client()
        {
            client = new TcpClient();
            client.Connect("127.0.0.1", 8888);
            netstream = client.GetStream();
            StreamWriter writer = new StreamWriter(netstream);
            writer.WriteLine();
            writer.Flush();
        }
    }
}
