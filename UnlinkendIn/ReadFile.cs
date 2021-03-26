using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnlinkendIn
{
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
            for (int i = 1; i <= N; i++)
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
            adjacencyMatrix = new bool[numVar + 1, numVar + 1];
            int idx1, idx2;
            string[] words;
            int N = Convert.ToInt16(textLines[0]);
            for (int i = 1; i <= N; i++)
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
    }
}
