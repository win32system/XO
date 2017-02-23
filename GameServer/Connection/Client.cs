using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Client
    {
        public TcpClient client;
        public NetworkStream netStream;
        public string name;
        public bool inGame;
        public bool isBusy;
        public int type;

        public Client(TcpClient client)
        {
            this.client = client;
            netStream = this.client.GetStream();
            inGame = false;
            isBusy = false;
        }
        public void Write(string message)
        {
            if (type == 1)
            {
                StreamWriter sw = new StreamWriter(client.GetStream());
                sw.WriteLine(message);
                sw.Flush();
            }
            else
            {
                byte[] response = Encoding.UTF8.GetBytes("  " + message);
                response[0] = 0x81; // denotes this is the final message and it is in text
                response[1] = Convert.ToByte(response.Length - 2); // payload size = message - header size
                client.GetStream().Write(response, 0, response.Length);
            }
        }

        public string Read()
        {
            if (type == 1)
            {
                StreamReader sr = new StreamReader(client.GetStream());
                return sr.ReadLine();
            }
            else
            {
                byte[] bytes1 = new byte[client.Available];

                client.GetStream().Read(bytes1, 0, bytes1.Length);

                string inp = DecodeMessage(bytes1);
                return inp;
            }
        }
        public string DecodeMessage(byte[] bytes)
        {
            string incomingData = string.Empty;
            byte secondByte = bytes[1];
            int dataLength = secondByte & 127;
            int indexFirstMask = 2;
            if (dataLength == 126)
                indexFirstMask = 4;
            else if (dataLength == 127)
                indexFirstMask = 10;

            IEnumerable<byte> keys = bytes.Skip(indexFirstMask).Take(4);
            int indexFirstDataByte = indexFirstMask + 4;

            byte[] decoded = new byte[bytes.Length - indexFirstDataByte];
            for (int i = indexFirstDataByte, j = 0; i < bytes.Length; i++, j++)
            {
                decoded[j] = (byte)(bytes[i] ^ keys.ElementAt(j % 4));
            }

            return incomingData = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
        }
    }
}
