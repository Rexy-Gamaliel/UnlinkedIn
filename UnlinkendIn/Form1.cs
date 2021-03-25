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
using Microsoft.Msagl.Drawing;

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
                ReadFile reader = new ReadFile(browse.FileName);

                int num = reader.GetNum();
                list = reader.GetVarList();
                Dictionary<string, int> dictionary = reader.GetDictionary();
                bool[,] matrix = reader.GetMatrix();

                foreach (string a in list)
                {
                    comboBox1.Items.Add(a);
                    comboBox2.Items.Add(a);
                }
                //create a graph object 
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                //create the graph content 
                for (int i=0; i<num; i++)
                { 
                    for (int j=0; j<i+1; j++)
                    {
                        if (matrix[i, j]==true)
                        {
                            string node1 = dictionary.FirstOrDefault(x => x.Value == i).Key;
                            string node2 = dictionary.FirstOrDefault(x => x.Value == j).Key;
                            graph.AddEdge(node1,node2).Attr.ArrowheadAtTarget = ArrowStyle.None;
                            graph.FindNode(node1).Attr.FillColor = Microsoft.Msagl.Drawing.Color.GreenYellow;
                            graph.FindNode(node2).Attr.FillColor = Microsoft.Msagl.Drawing.Color.GreenYellow;
                        }
                    }
                }
                //bind the graph to the viewer 
                gViewer1.Graph = graph;
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
