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
        RequestObject info;

        public EventHandler Answer;
        public EventHandler Cancle;
        public EventHandler Wait;

        public HandShake(Client client)
        {
            this.client = client;
            info = new RequestObject();
            info.Module = "HandShake";
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

        public void SendInvite(string userName, string gameName)
        {
            info.Cmd = "Invite";
            object[] arg = new object[] { userName, gameName };
            
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
        public void SendOk(string userName, string gameName)
        {
            info.Cmd = "Ok";
            object[] arg = new object[] { userName, gameName };

            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }

        public void SendCancle(string userName)
        {
            info.Cmd = "Cancle";
            object[] arg = new object[] { userName };
            
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
