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
        private void Send(RequestObject info)
        {
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(JsonConvert.SerializeObject(info));
            writer.Flush();

        }
        public void SendInvite(object arg)
        {
            Send(new RequestObject("HandShake", "Invite", arg)); 
        }
        public void SendOk(object arg)
        {
            Send(new RequestObject("HandShake", "Ok", arg));
          
        }

        public void SendCancle(object arg)
        {
            Send(new RequestObject("HandShake", "Cancle", arg));
        }
    }
}
