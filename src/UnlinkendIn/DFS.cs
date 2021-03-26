using System;
using System.Collections.Generic;
using System.Text;

namespace UnlinkendIn
{
    public class DFS
    {
        //atribut
        private int numVar;                                     // banyak simpul
        private string[] varList;                               // list simpul
        private Dictionary<string, int> varDictionary;
        private Stack<string> varStack;
        private bool[,] matriks;                                // matriks keterhubungan
        private string awal;                                    // simpul awal
        private string akhir;                                   // simpul akhir
        private bool[] visited;


        public DFS(string awal, string akhir, int numVar, string[] varList, Dictionary<string, int> varDictionary, bool[,] matriks)
        {
            this.awal = awal;
            this.akhir = akhir;
            this.numVar = numVar;
            this.varList = varList;
            this.varDictionary = varDictionary;
            this.varStack = new Stack<string>();
            this.matriks = matriks;
            this.visited = new bool[numVar];
            for (int i = 0; i < numVar; i++)
            {
                visited[i] = false;
            }
        }

        public Stack<string> ConstructPath()
        {
            Stack<string> path = new Stack<string>();

            if (awal.Equals(akhir))
            {
                path.Push(awal);
                return path;
            }

            Dictionary<string, string> precNode = new Dictionary<string, string>();
            // mencatat node sebelumnya dari sebuah node
            int currentIdx = getIdx(awal);
            int dIdx = getIdx(akhir);

            visited[currentIdx] = true;
            varStack.Push(awal);

            while (varStack.Count > 0)
            {
                // indeks node yang sedang dievaluasi
                currentIdx = getIdx(varStack.Pop());

                // jika node tujuan ditemukan
                if (currentIdx == dIdx)
                {
                    break;
                }


                int j = 0;
                // mengevaluasi setiap node yang bertetanggan dengan currentNode
                while (j < numVar)
                {
                    int othersIdx = 0;
                    bool found = false;
                    // memprioritaskan berdasarkan abjad
                    while (othersIdx < numVar && !found)
                    {
                        if (matriks[currentIdx, othersIdx] && !visited[othersIdx])
                        {
                            visited[othersIdx] = true;
                            varStack.Push(getName(othersIdx));
                            precNode.Add(getName(othersIdx), getName(currentIdx));
                            currentIdx = othersIdx;
                            found = true;
                        }
                        othersIdx++;
                    }
                    j++;
                }
            }

            // menyimpan jalur ke dalam stack
            if (precNode.ContainsKey(akhir))
            {
                string tempStr = akhir;
                path.Push(tempStr);
                while (tempStr != awal)
                {
                    tempStr = precNode[tempStr];
                    path.Push(tempStr);
                }
                return path;
            }
            else
            {
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
