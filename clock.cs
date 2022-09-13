using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTUDMIS62
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //int hour = Convert.ToInt32(lblHours.Text);
            //int minute = Convert.ToInt32(lblMinute.Text);
            //int second = Convert.ToInt32(lblSecond.Text);
            int second = DateTime.Now.Second;
            int minute = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            second++;
            if(second > 59)
            {
                second = 0;
                minute++;
            }
            if(second < 10)
            {
                lblSecond.Text = "0" + second;
            }
            else
            {
                lblSecond.Text = "" + second;
            }
            if(minute > 59)
            {
                minute = 0;
                minute++;
            }
            if(minute < 10)
            {
                lblMinute.Text = "0" + minute;
            }
            else 
            {
                lblMinute.Text = "" + minute;
            }
            if(hour < 10)
            {
                lblHours.Text = "0" + hour;
            }
            else
            {
                lblHours.Text = "" + hour;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
