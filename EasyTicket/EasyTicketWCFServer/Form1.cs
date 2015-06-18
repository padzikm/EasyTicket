using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
        public Form1()
        {
            InitializeComponent();
            timer.Interval =  1000 * 60 * int.Parse(frequencyTextBox.Text);
            timer.Start();
        }

        private async void TimerTick(object sender, EventArgs e)
        {
            //client.CheckNewOffer();
            await client.CheckOffersAsync();
        }

        private void SendMailClick(object sender, EventArgs e)
        {
            client.SendMailToAllAsync(mailBody.Text);
        }

        private void Form1Closing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
            client.Close();
        }

        private void StartCheckingClick(object sender, EventArgs e)
        {
            timer.Start();
            StartChecking.Enabled = false;
            stopChecking.Enabled = true;
        }

        private void stopChecking_Click(object sender, EventArgs e)
        {
            timer.Stop();
            StartChecking.Enabled = true;
            stopChecking.Enabled = false;
        }

        private void frequencyTextBox_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = int.Parse(frequencyTextBox.Text) * 1000 * 60;
        }
    }
}
