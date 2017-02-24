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

       

        public Game(Client client)
        {
            roomdialog = new RoomDialog();
            this.client = client;
          
          
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
            roomdialog.Init(client, Args);
            
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

        

        void OpenForm()
        {
            roomdialog.ShowDialog();
        }
    }
}
