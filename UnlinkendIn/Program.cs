using System;
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


            ReadFile reader = new ReadFile();

            int numVar = reader.getNum();
            string[] list = reader.getVariables();
            Dictionary<string, int> dictionary = reader.getDictionary();
                // gunakan dictionary[key] untuk mendapatkan value
            bool[][] matrix = reader.getMatrix();

            int i = 0;
            foreach (var variable in list) {
                Console.WriteLine("Index of {0} in list: {1}", variable, i);
                Console.WriteLine("Index of {0} in dict: {1}", variable, dictionary[variable]);
                i++;
            }
        }
    }

    public class ReadFile {
        // Attributes
        private string[] textLines;       // hasil parsing .txt per line
        private string[] variables;       // list of variable
        private Dictionary<string, int> varIndices;   // dictionary of <variable, index>
        private int numVar;               // banyak variable
        private bool[][] adjacencyMatrix; // adjacency matrix berukuran numVar x numVar

        public ReadFile() {
            textLines = File.ReadAllLines(@"./input.txt");
            parseVariables();
            constructGraph();
        }
        public int getNum() {
            return numVar;
        }
        public string[] getVariables() {
            return variables;
        }
        public Dictionary<string, int> getDictionary() {
            return varIndices;
        }
        public bool[][] getMatrix() {
            return adjacencyMatrix;
        }

        private void parseVariables() {
            string[] temp = new string[50];
            int count = 0;
            foreach (var line in textLines) {             // baca per baris
                string[] currentVars = line.Split(' ');     // array of string dari variabel di setiap baris
                foreach (var word in currentVars) {
                    if (!(isStringInArray(temp, word))) {     // jika variabel belum tercatat
                        temp[count] = word;
                        count++;
                    }
                }
            }

            numVar = count;
            // selesai mengakuisisi setiap variabel
            Console.WriteLine("> Test 1");
            variables = new string[numVar];
            for (int i = 0; i < numVar; i++) {
                variables[i] = temp[i];
            }
            Array.Sort(variables);

            varIndices = new Dictionary<string, int>();
            for (int i = 0; i < numVar; i++) {
                string word = variables[i];
                try {
                    varIndices.Add(word, count);
                }
                catch (ArgumentException) {
                    // no handling needed
                }
            }
        }

        private void constructGraph() {
            adjacencyMatrix = new bool[numVar][numVar];
            int idx1, idx2;
            string[] words;
            foreach(var line in textLines) {
                words = line.Split(' ');
                idx1 = varIndices[words[0]];
                idx2 = varIndices[words[1]];
                adjacencyMatrix[idx1][idx2] = true;
                adjacencyMatrix[idx2][idx1] = true;
            }
        }

        public bool isStringInArray(String[] array, string word)  {
            return Array.Exists(array, e => e == word);
        }
    }
}
