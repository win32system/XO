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
    public class Game
    {
        public EventHandler Close;
        public EventHandler Enabled;

        public RoomDialog roomdialog;
        Client client;
        string gameIndex;

        Info info;

        public Game(Client client)
        {
            roomdialog = new RoomDialog();
            this.client = client;
            info = new Info();
            info.Module = "Game";
        }

        public void Dispacher(Info info)
        {
            switch(info.Command)
            {
                case "Start":
                    Start(info.Message[0], info.Message[1]);
                    Close(null, null);
                    Enabled(null, null);
                    break;
                case "Move":
                    Move(info.Message);
                    break;
                case "Over":
                    End();
                    break;
            }
        }

        private void Start(string gameIndex, string gameName)
        {
            this.gameIndex = gameIndex;
            roomdialog.Init(client, gameName);
            if (roomdialog.game is XO)
                roomdialog.game.MouseDown += SendMoveXO;
            Thread open = new Thread(new ThreadStart(OpenForm));
            open.Start();
        }

        private void End()
        {
            roomdialog.End();
        }

        private void Move(List<string> move)
        {
            roomdialog.Draw(move);
        }

        private void SendMoveXO(object sender, MouseEventArgs e)
        {
            string x = "";
            string y = "";
            if (e.X >= 70 && e.X <= 120)
                y = "0";
            if (e.X >= 120 && e.X <= 170)
                y = "1";
            if (e.X >= 170 && e.X <= 220)
                y = "2";


            if (e.Y >= 70 && e.Y <= 120)
                x = "0";
            if (e.Y >= 120 && e.Y <= 170)
                x = "1";
            if (e.Y >= 170 && e.Y <= 220)
                x = "2";
            if(x!="" && y!="")
            {
                info.Command = "Move";
                info.Message.Clear();
                info.Message.Add(gameIndex);
                info.Message.Add("" + x);
                info.Message.Add("" + y);

                string strInfo = JsonConvert.SerializeObject(info);
                StreamWriter writer = new StreamWriter(client.netstream);
                writer.WriteLine(strInfo);
                writer.Flush();
            }
        }

        void OpenForm()
        {
            roomdialog.ShowDialog();
        }
    }
}
