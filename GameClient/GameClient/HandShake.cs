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
        Info info;

        public EventHandler Answer;
        public EventHandler Cancle;
        public EventHandler Wait;

        public HandShake(Client client)
        {
            this.client = client;
            info = new Info();
            info.Module = "HandShake";
        }

        public void Dispacher(Info tmpinfo)
        {
            switch (tmpinfo.Command)
            {
                case "Invited":
                    Answer(tmpinfo.Message, null);
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
            info.Command = "Invite";
            info.Message.Clear();
            info.Message.Add(userName);
            info.Message.Add(gameName);
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
        public void SendOk(string userName, string gameName)
        {
            info.Command = "Ok";
            info.Message.Clear();
            info.Message.Add(userName);
            info.Message.Add(gameName);
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }

        public void SendCancle(string userName)
        {
            info.Command = "Cancle";
            info.Message.Clear();
            info.Message.Add(userName);
            string strInfo = JsonConvert.SerializeObject(info);
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(strInfo);
            writer.Flush();
        }
    }
}
