using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnlinkendIn
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            KAMUS:
            num         : int                       { banyak variable }
            list        : array of string           { berisi nama-nama variabel }
            dictionary  : dictionary<string, int>   { berisi pasangan <variabel, index> untuk memudahkan pencarian index }
            matrix      : bool[numVar][numVar]      { adjacency matrix }
            */

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class ReadFile
    {
        // Attributes
        
        private string[] textLines;       // hasil parsing .txt per line
        private string[] variables;       // list of variable
        private Dictionary<string, int> varIndices;   // dictionary of <variable, index>
        private int numVar;               // banyak variable
        private bool[,] adjacencyMatrix; // adjacency matrix berukuran numVar x numVar

        public ReadFile(string file)
        {
            textLines = File.ReadAllLines(file);
            ParseVariables();
            ConstructGraph();
        }

        public int GetNum()
        {
            return numVar;
        }
        public string[] GetVarList()
        {
            return variables;
        }
        public Dictionary<string, int> GetDictionary()
        {
            return varIndices;
        }
        public bool[,] GetMatrix()
        {
            return adjacencyMatrix;
        }

        private void ParseVariables()
        {
            string[] temp = new string[50];
            int count = 0;
            int N = Convert.ToInt16(textLines[0]);
            for (int i=1; i<=N; i++)
            {
                string[] currentVars = textLines[i].Split(' ');     // array of string dari variabel di setiap baris
                foreach (var word in currentVars)
                {
                    if (!(IsStringInArray(temp, word)))
                    {     // jika variabel belum tercatat
                        temp[count] = word;
                        count++;
                    }
                }
            }

            numVar = count;
            // selesai mengakuisisi setiap variabel
            Console.WriteLine("> Test 1");
            variables = new string[numVar];
            for (int i = 0; i < numVar; i++)
            {
                variables[i] = temp[i];
            }
            Array.Sort(variables);

            varIndices = new Dictionary<string, int>();
            for (int i = 0; i < numVar; i++)
            {
                string word = variables[i];
                try
                {
                    varIndices.Add(word, count);
                }
                catch (ArgumentException)
                {
                    // no handling needed
                }
            }
        }

        private void ConstructGraph()
        {
            adjacencyMatrix = new bool[numVar+1, numVar+1];
            int idx1, idx2;
            string[] words;
            int N = Convert.ToInt16(textLines[0]);
            for (int i=1; i<=N; i++)
            {
                words = textLines[i].Split(' ');
                idx1 = varIndices[words[0]];
                idx2 = varIndices[words[1]];
                adjacencyMatrix[idx1, idx2] = true;
                adjacencyMatrix[idx2, idx1] = true;
            }
        }

        public bool IsStringInArray(String[] array, string word)
        {
            return Array.Exists(array, e => e == word);
        }
    }

    public class Friend_Recommendation
    {
        // atribut
        private int[] jumlah_keterhubungan;     // Banyak nEff yang dimiliki 
        private int[] mutual_friends;           // Matriks nama mutual friends untuk setiap final node


        // mendapatkan array yang berisikan 
        // find_friends merupakan indeks initial variabel yang ingin dicari 
        public void process(int find_friends)
        {
            arr_dikunjungi[find_friends] = true;

            for (int i = 0; i < length; i++)    // length = panjang baris efektif yang ingin dicari
            {
                for (int j = 0; j < length; j++)// length = panjang baris efektif dari daun pertama
                {
                    // node belum dikunjungi dan tidak berjarak satu dengan node initial
                    // is_index_in_array mirip dengan IsStringInArray tapi bingung containernya bentuk nya seperti apa
                    if (!(arr_dikunjungi[j] || is_index_in_array(arr_int[i], j)))
                    {
                        mutual_friends[j][jumlah_keterhubungan[j]] = arr_int[i];
                        jumlah_keterhubungan[j]++;
                    }
                }
            }
        }

        public void display()
        {
            // masih belum terurut dengan jumlah mutual friends terbesar
            for (int i = 0; i < ReadFile.GetNum(); i++)
            {
                if (jumlah_keterhubungan[i] > 0)
                {
                    Console.WriteLine("Nama akun: " + arr_string[i]);
                    Console.WriteLine(jumlah_keterhubungan[i] + " mutual friends: ");

                    foreach (string item in mutual_friends[i])
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }

    public class BFS
    {
        private int numVar;
        private string[] varList;
        private Dictionary<string, int> varDictionary;
        private Queue<string> varQueue;
        private bool[,] matrix;
        private string sNode;
        private string dNode;
        private bool[] visited;

        public BFS (string _sNode, string _dNode, int _numVar, string[] _varList, Dictionary<string, int> _varDictionary, bool [,] _matrix)
        {
            this.numVar = _numVar;
            this.varList = _varList;
            this.varDictionary = _varDictionary;
            this.varQueue = new Queue<string>();
            this.matrix = _matrix;
            this.sNode = _sNode;
            this.dNode = _dNode;
            this.visited = new bool[numVar];
            for (int i = 0; i < numVar; i++)
            {
                visited[i] = false;
            }
        }

        public Stack<string> ConstructPath()
        {
            Stack<string> path = new Stack<string>();

            if (sNode.Equals(dNode))
            {
                path.Push(sNode);
                return path;
            }

            Dictionary<string, string> precNode = new Dictionary<string, string>();
            // mencatat node sebelumnya dari sebuah node
            int currentIdx = getIdx(sNode);
            int dIdx = getIdx(dNode);

            visited[currentIdx] = true;
            varQueue.Enqueue(sNode);
            // int it = 1;

            while (varQueue.Count > 0)
            {
                // Console.Out.WriteLine("Iteration " + it + " : ");
                // indeks node yang sedang dievaluasi
                currentIdx = getIdx(varQueue.Dequeue());
                // Console.Out.WriteLine("Current Node: " + getName(currentIdx));

                // jika node tujuan ditemukan
                if (currentIdx == dIdx)
                {
                    break;
                }

                // mengevaluasi setiap node yang bertetanggan dengan currentNode
                for (int othersIdx = 0; othersIdx < numVar; othersIdx++)
                {
                    if (matrix[currentIdx, othersIdx] && !visited[othersIdx])
                    {
                        visited[othersIdx] = true;
                        varQueue.Enqueue(getName(othersIdx));
                        precNode.Add(getName(othersIdx), getName(currentIdx));
                    }
                }
                // it++;
            }

            // construct path
            // testing
            // foreach (KeyValuePair<string, string> item in precNode)
            // {
            //     Console.Out.WriteLine(item.Key + " : " + item.Value);
            // }
            //

            // backtracking
            if (precNode.ContainsKey(dNode))
            {
                string tempStr = dNode;
                path.Push(tempStr);
                while (tempStr != sNode)
                {
                    tempStr = precNode[tempStr];
                    path.Push(tempStr);
                }
                return path;
            }
            else
            {
                path.Push("NULL");
                return path;
            }
        }

        private int getIdx(string node)
        {
            return varDictionary[node];
        }
        private string getName(int idx)
        {
            return varList[idx];
        }
    }
}
