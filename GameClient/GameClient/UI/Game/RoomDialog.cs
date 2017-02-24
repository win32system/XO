using GameServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class RoomDialog : Form
    {
        public UserControl game;
        private Client client;
        private object gameIndex;
        public RoomDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
        }
        public void Init(Client client, object Args)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());
            if (args[1].ToString() == "XO")
            {
                this.client = client;
                this.gameIndex = args[0];
            }
        }

        public void Draw(object Args)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(Args.ToString());
            //if (game is XO)
              //  (game as XO).Draw(args[0].ToString(), args[1].ToString(), args[2].ToString());
        }
        private void SendMoveXO(object sender, EventArgs e)
        {
            string str = (sender as Button).Tag.ToString();
            if (str != "")
            {
                StreamWriter writer = new StreamWriter(client.netstream);
                string strInfo = JsonConvert.SerializeObject(new RequestObject("Game", "Move", new object[] { gameIndex, str }));
                writer.WriteLine(strInfo);
                writer.Flush();
            }
        }
        public void End()
        {
            MessageBox.Show("Game over");
            Close();
        }

    
    }
}
