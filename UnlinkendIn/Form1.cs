using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UnlinkendIn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                label10.Text = System.IO.Path.GetFileName(browse.FileName);
                //string[] lines = File.ReadAllLines(browse.FileName);
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(browse.FileName).ToList();
                int N = Convert.ToInt16(lines[0]);
                
                foreach (String line in lines)
                {
                    textBox1.Text += line;
                    textBox1.Text += Environment.NewLine;
                }

                Console.ReadLine();
            
            // finding index
            //int index = myList.FindIndex(a => a.Contains("Tennis"));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
