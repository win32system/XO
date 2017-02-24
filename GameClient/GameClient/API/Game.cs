using GameServer;
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

        RequestObject info;

        public Game(Client client)
        {
            roomdialog = new RoomDialog();
            this.client = client;
            info = new RequestObject();
            info.Module = "Game";
        }

        public void Dispacher(RequestObject info)
        {
            switch(info.Cmd)
            {
                case "Start":
                    Start(info.Args);
                    Close(null, null);
                    Enabled(null, null);
                    break;
                case "Move":
                    Move(info.Args);
                    break;
                case "Over":
                    End();
                    break;
            }
        }

        private void Start(object Args)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());
            this.gameIndex = args[0].ToString();
            roomdialog.Init(client, args[1].ToString());
        
                roomdialog.game.MouseDown += SendMoveXO;
            Thread open = new Thread(new ThreadStart(OpenForm));
            open.Start();
        }

        private void End()
        {
            roomdialog.End();
        }

        private void Move(object Args)
        {
            roomdialog.Draw(Args);
        }

        private void SendMoveXO(object sender, MouseEventArgs e)
        {
            string str = (sender as Button).Tag.ToString();

            if (str == "")
                return;

            RequestObject res = new RequestObject("Game", "Move", new object[] { gameIndex, str });
            StreamWriter writer = new StreamWriter(client.netstream);
            writer.WriteLine(JsonConvert.SerializeObject(res));
            writer.Flush();
        }

        void OpenForm()
        {
            roomdialog.ShowDialog();
        }
    }
}
