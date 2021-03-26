using System;
using System.Collections.Generic;
using System.Text;

namespace UnlinkendIn
{
    public class Friend_Recommendation
    {
        // atribut
        private string id;                          // node awal / yang ingin dicari\
        private int id_main;
        private Dictionary<string, int> varDictionary;
        private int total_var;                      // banyak variable 
        private string[] list_var;                  // list of variable [A, B, C, D, E, F]
        private bool[,] graph_link;                 // matriks keterhubungan boolean
        private bool[] link_visited;                // array yang telah dikunjungi
        private int[] total_linked;                 // Banyak nEff matriks mutual friends yang dimiliki 
        private string[] mutual_friends;            // Matriks nama mutual friends untuk setiap final node

        public Friend_Recommendation(string id, int total_var,
            string[] list_var, Dictionary<string, int> dictionary_f,
            bool[,] graph_link)
        {

            this.id = id;
            this.id_main = getIdx(id);
            this.total_var = total_var;
            this.list_var = list_var;
            this.graph_link = graph_link;
            this.varDictionary = dictionary_f;

            /* assign array link_visited dengan false */
            this.link_visited = new bool[this.total_var];
            for (int i = 0; i < total_var; i++)
            {
                this.link_visited[i] = false;
            }

            /* assign total linked yang kosong dengan nilai 0 */
            this.total_linked = new int[this.total_var];
            for (int i = 0; i < total_var; i++)
            {
                this.total_linked[i] = 0;
            }

            /* assign array indeks mutual friends dengan nilai -1 */
            this.mutual_friends = new string[this.total_var];
            for (int i = 0; i < total_var; i++)
            {
                this.mutual_friends[i] = "";
            }
        }

        /* Mengisikan array total_linked yang berisikan 
         * jumlah mutual friends setiap final node
         * dan mengisikan array mutual friends dengan 
         * string nama mutual friends */
        public void process()
        {
            for (int i = 0; i < this.total_var; i++)
            {
                // graph terhubung jarak pertama
                if (this.graph_link[this.id_main,i])
                {
                    for (int j = 0; j < this.total_var; j++)
                    {
                        // graph terhubung jarak kedua
                        if (this.graph_link[i,j])
                        {
                            // node apakah sudah dikunjungi
                            if (!this.link_visited[j])
                            {
                                this.link_visited[j] = true;
                                this.mutual_friends[total_linked[j]] = this.list_var[j];
                                this.total_linked[j]++;
                            }
                        }
                    }
                }

            }

        }

        /* Menampilkan mutual friends dan jumlah mutual friends ke layar 
         * dari array idx mutual_friends, list_var dan total_linked */
        public void display()
        {
            // masih belum terurut dengan jumlah mutual friends terbesar
            for (int i = 0; i < this.total_var; i++)
            {
                if (this.total_linked[i] > 0)
                {
                    Console.WriteLine("Nama akun: " + this.list_var[i]);
                    Console.WriteLine(this.total_linked[i] + " mutual friends: ");

                    for (int j = 0; j < this.total_linked[i]; j++)
                    {
                        Console.WriteLine(this.mutual_friends[i]);
                    }
                    Console.WriteLine("\n");
                }
            }
        }
        private int getIdx(string node)
        {
            return varDictionary[node];
        }

        public int[] get_total_linked()
        {
            return this.total_linked;
        }

        public string[] get_mutual_friends()
        {
            return this.mutual_friends;
        }

    }
}
