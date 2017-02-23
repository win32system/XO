using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class AnswerForm : Form
    {
        HandShake handShake;
        List<string> info;

        public AnswerForm(HandShake handShake, List<string> info)
        {
            InitializeComponent();
            this.handShake = handShake;
            label1.Text = info[0] + " приглашает вас поиграть в " + info[1];
            this.info = info;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            handShake.SendOk(info[0], info[1]);
            this.Close();
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            handShake.SendCancle(info[0]);
            this.Close();
        }
    }
}
