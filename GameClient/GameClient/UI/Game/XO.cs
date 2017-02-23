using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class XO : UserControl
    {
        Client client;

        public XO(Client client)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.client = client;
        }

        private void XO_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            e.Graphics.DrawLine(pen, 120, 70, 120, 220);
            e.Graphics.DrawLine(pen, 170, 70, 170, 220);
            e.Graphics.DrawLine(pen, 70, 120, 220, 120);
            e.Graphics.DrawLine(pen, 70, 170, 220, 170);
        }

        private void XO_MouseDown(object sender, MouseEventArgs e)
        {
        }
        public void Draw(string x, string y, string figure)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            

            if (x == "0" && y == "0")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 75, 75, 115, 115);
                    g.DrawLine(pen, 115, 75, 75, 115);
                }
                else
                {
                    g.DrawEllipse(pen, 75, 75, 40, 40);
                }
            }
            if (x == "0" && y == "1")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 125, 75, 165, 115);
                    g.DrawLine(pen, 165, 75, 125, 115);
                }
                else
                {
                    g.DrawEllipse(pen, 125, 75, 40, 40);
                }
            }
            if (x == "0" && y == "2")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 175, 75, 215, 115);
                    g.DrawLine(pen, 215, 75, 175, 115);
                }
                else
                {
                    g.DrawEllipse(pen, 175, 75, 40, 40);
                }
            }
            if (x == "1" && y == "0")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 75, 125, 115, 165);
                    g.DrawLine(pen, 115, 125, 75, 165);
                }
                else
                {
                    g.DrawEllipse(pen, 75, 125, 40, 40);
                }
            }
            if (x == "1" && y == "1")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 125, 125, 165, 165);
                    g.DrawLine(pen, 165, 125, 125, 165);
                }
                else
                {
                    g.DrawEllipse(pen, 125, 125, 40, 40);
                }
            }
            if (x == "1" && y == "2")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 175, 125, 215, 165);
                    g.DrawLine(pen, 215, 125, 175, 165);
                }
                else
                {
                    g.DrawEllipse(pen, 175, 125, 40, 40);
                }
            }
            if (x == "2" && y == "0")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 75, 175, 115, 215);
                    g.DrawLine(pen, 115, 175, 75, 215);
                }
                else
                {
                    g.DrawEllipse(pen, 75, 175, 40, 40);
                }
            }
            if (x == "2" && y == "1")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 125, 175, 165, 215);
                    g.DrawLine(pen, 165, 175, 125, 215);
                }
                else
                {
                    g.DrawEllipse(pen, 125, 175, 40, 40);
                }
            }
            if (x == "2" && y == "2")
            {
                if (figure == "X")
                {
                    g.DrawLine(pen, 175, 175, 215, 215);
                    g.DrawLine(pen, 215, 175, 175, 215);
                }
                else
                {
                    g.DrawEllipse(pen, 175, 175, 40, 40);
                }
            }
        }
    }
}
