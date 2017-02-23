using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public RoomDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public void Init(Client client, string gameName)
        {
            switch (gameName)
            {
                case "XO":
                    game = new XO(client);
                    break;
            }
            this.Controls.Add(game);
        }

        public void Draw(List<string> move)
        {
            if (game is XO)
                (game as XO).Draw(move[0], move[1], move[2]);
        }

        public void End()
        {
            MessageBox.Show("Game over");
            Close();
        }
    }
}
