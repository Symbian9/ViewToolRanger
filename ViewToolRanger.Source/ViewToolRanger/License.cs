using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace ShowLicense
{
    public partial class License : Form
    {
        


        public License(string licensefile)
        {
            InitializeComponent();
            

            string[] all_lines = null;

            if (File.Exists(licensefile) == true)
            {
                all_lines = File.ReadAllLines(licensefile);
            }
            

            foreach (string s in all_lines)
            {
                listBox1.Items.Add(s);
            }


        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDisagree_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
