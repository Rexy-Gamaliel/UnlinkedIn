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
        private string[] list;
        public Form1()
        {
            InitializeComponent();
            OpenFileDialog browse = new OpenFileDialog();
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
                ///List<string> lines = new List<string>();
                ///lines = File.ReadAllLines(browse.FileName).ToList();
                ///int N = Convert.ToInt16(lines[0]);
                
                ///foreach (string line in lines)
                ///{
                ///    textBox1.Text += line;
                ///   textBox1.Text += Environment.NewLine;
                ///}

                ///Console.ReadLine();
                ReadFile reader = new ReadFile(browse.FileName);

                int num = reader.GetNumVar();
                list = reader.GetVarList();
                Dictionary<string, int> dictionary = reader.GetDictionary();
                // gunakan dictionary[key] untuk mendapatkan value
                bool[,] matrix = reader.GetMatrix();

                foreach (string a in list)
                {
                    comboBox1.Items.Add(a);
                    comboBox2.Items.Add(a);
                }

            // finding index
            //int index = myList.FindIndex(a => a.Contains("Tennis"));
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fitur = comboBox3.SelectedItem.ToString();
            if (fitur == "Friend Recommendation")
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = true;
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                comboBox2.Enabled = false;
            }

            if (fitur == "Explorer Friends")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox2.Enabled = true;

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            vScrollBar1.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (string a in list)
            {
                comboBox2.Items.Add(a);
            }
            comboBox2.Items.Remove(comboBox1.SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string a in list)
            {
                comboBox1.Items.Add(a);
            }
            comboBox1.Items.Remove(comboBox2.SelectedItem);
        }
    }
}
