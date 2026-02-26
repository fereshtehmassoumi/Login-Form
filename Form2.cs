using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form2 : Form
    {

        string username;
        public Form2( string name)
        {
       username = name;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text += " "+ username + "!";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
