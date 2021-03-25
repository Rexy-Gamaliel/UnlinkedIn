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
        private int numVar;
        private Dictionary<string, int> dictionary;
        private bool[,] matrix;
        private Graph graph;

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
                comboBoxFitur.Enabled = true;
                label10.Text = System.IO.Path.GetFileName(browse.FileName);
                ReadFile reader = new ReadFile(browse.FileName);

                numVar = reader.GetNumVar();
                list = reader.GetVarList();
                dictionary = reader.GetDictionary();
                matrix = reader.GetMatrix();
                
                comboBoxAkunAwal.Items.Clear();
                comboBoxAkunAkhir.Items.Clear();

                foreach (string a in list)
                {
                    comboBoxAkunAwal.Items.Add(a);
                    comboBoxAkunAkhir.Items.Add(a);
                }
                //create a graph object 
                graph = new Microsoft.Msagl.Drawing.Graph("graph");
                //create the graph content 
                for (int i=0; i< numVar; i++)
                { 
                    for (int j=0; j<i+1; j++)
                    {
                        if (matrix[i, j]==true)
                        {
                            string node1 = dictionary.FirstOrDefault(x => x.Value == i).Key;
                            string node2 = dictionary.FirstOrDefault(x => x.Value == j).Key;
                            graph.AddEdge(node1,node2).Attr.ArrowheadAtTarget = ArrowStyle.None;
                        }
                    }
                }
                //bind the graph to the viewer 
                gViewer.Graph = graph;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fitur = comboBoxFitur.SelectedItem.ToString();
            if (fitur == "Friend Recommendation")
            {
                radioButtonDFS.Enabled = false;
                radioButtonBFS.Enabled = true;
                radioButtonDFS.Checked = false;
                radioButtonBFS.Checked = true;
                comboBoxAkunAwal.Enabled = true;
                comboBoxAkunAkhir.Enabled = false;
                comboBoxAkunAkhir.ResetText();
            }

            if (fitur == "Explorer Friends")
            {
                radioButtonDFS.Enabled = true;
                radioButtonBFS.Enabled = true;
                radioButtonDFS.Checked = true;
                radioButtonBFS.Checked = false;
                comboBoxAkunAwal.Enabled = true;
                comboBoxAkunAkhir.Enabled = true;
                comboBoxAkunAwal.ResetText();
                comboBoxAkunAkhir.ResetText();
            }

            if (fitur == "")
            {
                radioButtonDFS.Enabled = false;
                radioButtonBFS.Enabled = false;
                radioButtonDFS.Checked = false;
                radioButtonBFS.Checked = false;
                comboBoxAkunAwal.Enabled = false;
                comboBoxAkunAkhir.Enabled = false;
                comboBoxAkunAwal.ResetText();
                comboBoxAkunAkhir.ResetText();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            vScrollBar1.Visible = true;
            textBox2.Clear();

            foreach (string cek in list)
            {
                graph.FindNode(cek).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            }
            
            if (comboBoxFitur.SelectedItem.ToString()== "Friend Recommendation")
            {
                string akun = comboBoxAkunAwal.SelectedItem.ToString();
            }
            else if (comboBoxFitur.SelectedItem.ToString()== "Explorer Friends")
            {
                
               string awal = comboBoxAkunAwal.Text;
               string akhir = comboBoxAkunAkhir.Text;
                
               if (radioButtonBFS.Checked == true)
                {
                    BFS friendRecom = new BFS(awal,akhir,numVar,list,dictionary, matrix);
                    Stack<string> path = new Stack<string>();
                    path = friendRecom.ConstructPath();
                    int degree = path.Count - 2;
                    
                    if (path.Count == 0)
                    {
                        textBox2.Text += "Tidak ada jalur koneksi yang tersedia";
                        textBox2.Text += Environment.NewLine;
                        textBox2.Text += "Anda harus memulai koneksi baru itu sendiri.";
                    }
                    else if (degree == 0)
                    {
                        textBox2.Text += "Kedua akun sudah terkoneksi/terhubung.";
                        graph.FindNode(awal).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                        graph.FindNode(akhir).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        textBox2.Text += degree;
                        if (degree == 1)
                        {
                            textBox2.Text += "st";
                        }
                        else if (degree == 2)
                        {
                            textBox2.Text += "nd";
                        }
                        else if (degree == 3)
                        {
                            textBox2.Text += "rd";
                        }
                        else 
                        {
                            textBox2.Text += "th";
                        }
                        textBox2.Text += " Degree";
                        textBox2.Text += Environment.NewLine;
                        textBox2.Text += "Alternatif Jalur yang Tersedia";
                        textBox2.Text += Environment.NewLine;
                        while (path.Count > 1)
                        {
                            string curr = path.Pop();
                            textBox2.Text += curr + "-";
                            graph.FindNode(curr).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                        }
                        string end = path.Pop();
                        textBox2.Text += end;
                        graph.FindNode(end).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                    }
                    
                }
                else if (radioButtonDFS.Checked == true)
                {
                    DFS friendRecom = new DFS(awal, akhir, numVar, list, dictionary, matrix);
                    Stack<string> path = new Stack<string>();
                    path = friendRecom.ConstructPath();
                    int degree = path.Count - 2;

                    if (path.Count == 0)
                    {
                        textBox2.Text += "Tidak ada jalur koneksi yang tersedia";
                        textBox2.Text += Environment.NewLine;
                        textBox2.Text += "Anda harus memulai koneksi baru itu sendiri.";
                    }
                    else if (degree == 0)
                    {
                        textBox2.Text += "Kedua akun sudah terkoneksi/terhubung.";
                        graph.FindNode(awal).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                        graph.FindNode(akhir).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        textBox2.Text += degree;
                        if (degree == 1)
                        {
                            textBox2.Text += "st";
                        }
                        else if (degree == 2)
                        {
                            textBox2.Text += "nd";
                        }
                        else if (degree == 3)
                        {
                            textBox2.Text += "rd";
                        }
                        else
                        {
                            textBox2.Text += "th";
                        }
                        textBox2.Text += " Degree";
                        textBox2.Text += Environment.NewLine;
                        textBox2.Text += "Alternatif Jalur yang Tersedia";
                        textBox2.Text += Environment.NewLine;
                        while (path.Count > 1)
                        {
                            string curr = path.Pop();
                            textBox2.Text += curr + "-";
                            graph.FindNode(curr).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                        }
                        string end = path.Pop();
                        textBox2.Text += end;
                        graph.FindNode(end).Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
                    }

                }
                gViewer.Graph = graph;
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAkunAkhir.Items.Clear();
            foreach (string a in list)
            {
                comboBoxAkunAkhir.Items.Add(a);
            }
            comboBoxAkunAkhir.Items.Remove(comboBoxAkunAwal.SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAkunAwal.Items.Clear();
            foreach (string a in list)
            {
                comboBoxAkunAwal.Items.Add(a);
            }
            comboBoxAkunAwal.Items.Remove(comboBoxAkunAkhir.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBoxAkunAwal.Items.Clear();
            comboBoxAkunAkhir.Items.Clear();
            comboBoxAkunAwal.ResetText();
            comboBoxAkunAkhir.ResetText();
            comboBoxFitur.ResetText();
            comboBoxAkunAwal.SelectedIndex = -1;
            comboBoxAkunAkhir.SelectedIndex = -1;
            comboBoxFitur.SelectedIndex = 0;
            label10.Text = "";
            textBox2.Visible = false;
            vScrollBar1.Visible = false;
            comboBoxFitur.Enabled = false;
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph delgraph = new Microsoft.Msagl.Drawing.Graph("graph");
            //bind the graph to the viewer 
            gViewer.Graph = delgraph;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAkunAwal.Enabled = true;
            comboBoxAkunAkhir.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAkunAwal.Enabled = true;
            comboBoxAkunAkhir.Enabled = true;
        }
    }
}
