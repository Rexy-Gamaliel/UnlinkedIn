using System;
using System.Collections.Generic;
using System.Text;

namespace UnlinkendIn
{
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

        public BFS(string _sNode, string _dNode, int _numVar, string[] _varList, Dictionary<string, int> _varDictionary, bool[,] _matrix)
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
                // indeks node yang sedang dievaluasi
                currentIdx = getIdx(varQueue.Dequeue());

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
            }

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
