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

        public int GetNumVar()
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
                    varIndices.Add(word, i);
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

        public bool IsStringInArray(string[] array, string word)
        {
            return array.Contains(word);
        }

        public bool IsIndexInArray(int[] array, int num)
        {
            return array.Contains(num);
        }

        /*public class Friend_Recommendation
        {
            // atribut
            private int[] jumlah_keterhubungan;     // Banyak nEff yang dimiliki 
            private int[] mutual_friends;           // Matriks nama mutual friends untuk setiap final node
            private bool[] arr_dikunjungi;

            // mendapatkan array yang berisikan 
            // find_friends merupakan indeks initial variabel yang ingin dicari 
            public void process(int find_friends)
            {
                arr_dikunjungi[find_friends] = true;

                for (int i = 0; i < ReadFile.GetNumVar(); i++)    // length = panjang baris efektif yang ingin dicari
                {
                    for (int j = 0; j < ReadFile.GetNumVar(); j++)// length = panjang baris efektif dari daun pertama
                    {
                        // node belum dikunjungi dan tidak berjarak satu dengan node initial
                        // is_index_in_array mirip dengan IsStringInArray tapi bingung containernya bentuk nya seperti apa
                        if (!(arr_dikunjungi[j] || IsIndexInArray(arr_int[i], j)))
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
                for (int i = 0; i < ReadFile.GetNumVar(); i++)
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

        public class ExplorerFriend_DFS
        {
            //atribut
            private bool connected; // mengecek apakah kedua simpul terhubung
            private int awal; // indeks simpul awal
            private int akhir; // indeks simpul akhir
            private List<int> jalur; // jalur yang menghubungi
            private bool[] arr_dikunjungi; // simpul yang sudah dikunjungi
            private int n; // banyak simpul
            private bool[,] matriks; // matriks keterhubungan

            public ExplorerFriend_DFS(int awal, int akhir, bool[,] matriks, int n)
            {
                this.awal = awal;
                this.akhir = akhir;
                this.connected = false;
                this.jalur = new List<int>();
                this.arr_dikunjungi = new bool[n];
                this.matriks = new bool[n, n];

                for (int i=0; i<n; i++)
                {
                    for (int j=0; j<n; j++)
                    {
                        this.matriks[i, j] = matriks[i, j];
                    }
                }

                this.n = n;

                for (int i=0; i<n; i++)
                {
                    arr_dikunjungi[i] = false;
                }
            }

            public void DFSUtil(int curr)
            {
                jalur.Add(curr);
                arr_dikunjungi[curr] = true;
                for (int j=1; j<n; j++)
                {
                    if (matriks[curr, j] = true)
                    {
                        if (!(arr_dikunjungi[j]))
                        {
                            if (j == akhir)
                            {
                                this.connected = true;
                                break;
                            }
                            else
                            {
                                DFSUtil(j);
                            }
                        }
                    }
                }
            }

            public void PrintDFSUtil()
            {
                if (connected)
                {
                    foreach (int j in jalur)
                    {
                        Console.WriteLine(j);
                    }
                }
                else
                {
                    Console.WriteLine("Tidak ada koneksi");
                }
            }
        }*/
    }




}
