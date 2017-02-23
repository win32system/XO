using GameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class HandShake
    {
        Client client;
       
        public EventHandler Answer;
        public EventHandler Cancle;
        public EventHandler Wait;

        public HandShake(Client client)
        {
            this.client = client;
        }

        public void Dispacher(RequestObject tmpinfo)
        {
            switch (tmpinfo.Cmd)
            {
                case "Invited":
                    Answer(tmpinfo.Args, null);
                    break;
                case "Cancle":
                    Cancle(null, null);
                    break;
                case "Wait":
                    Wait(null, null);
                    break;
            }
        }
        public void Send(string cmd, object args)
        {
            StreamWriter writer = new StreamWriter(client.netstream);
            RequestObject info = new RequestObject("HandShake",cmd , args);
            writer.WriteLine(JsonConvert.SerializeObject(info));
            writer.Flush();
        }
        public void SendInvite(object args)
        {
            Send("Invite", args);
        }
        public void SendOk(object args)
        {
            Send("Ok", args);
        }

        public void SendCancle(object args)
        {
            Send("Cancle", args);
        }
    }
}
