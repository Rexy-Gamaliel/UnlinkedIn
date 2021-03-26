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
        private Queue<string> antrian;              // antrian
        private int total_var;                      // banyak variable 
        private string[] list_var;                  // list of variable [A, B, C, D, E, F]
        private bool[,] graph_link;                 // matriks keterhubungan boolean
        private bool[] link_visited;                // array yang telah dikunjungi
        private int[] total_linked;                 // Banyak nEff matriks mutual friends yang dimiliki 
        private string[,] mutual_friends;            // Matriks nama mutual friends untuk setiap final node

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
            this.antrian = new Queue<string>();

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
            this.mutual_friends = new string[this.total_var, this.total_var];
            for (int i = 0; i < total_var; i++)
            {
                for (int j = 0; j < total_var; j++)
                {
                    this.mutual_friends[i,j] = "";
                }
                    
            }
        }

        /* Mengisikan array total_linked yang berisikan 
         * jumlah mutual friends setiap final node
         * dan mengisikan array mutual friends dengan 
         * string nama mutual friends */
        public void process()
        {
            /* inisialisasi simpul awal dengan nilai true */
            this.link_visited[this.id_main] = true;

            /* memasukan simpul awal ke dalam antrian */
            this.antrian.Enqueue(this.list_var[this.id_main]);

            /* memasukan simpul - simpul tetangga ke dalam antrian
             * dan set nilai boolean kunjunginya menjadi true */
            for (int i = 0; i < this.total_var; i++)
            {
                if (this.graph_link[this.id_main, i])
                {
                    this.antrian.Enqueue(this.list_var[i]);
                    this.link_visited[i] = true;
                }
            }

            /* mengeluarkan simpul awal dari antrian */
            this.antrian.Dequeue();

            /* Mengakses antrian satu per satu */
            while (this.antrian.Count > 0)
            {
                /* graf level kedua untuk mencari final node dan mutual friends */
                for (int i = 0; i < this.total_var; i++)
                {
                    /* jika ada keterhubungan antara antrian pertama dengan pencarian node */
                    if (this.graph_link[this.getIdx(this.antrian.Peek()), i])
                    {
                        /* jika node belum dikunjungi */
                        if (!this.link_visited[i])
                        {
                            /* assign nilai string ke dalam matriks mutual friends */
                            this.mutual_friends[i, total_linked[i]] = this.antrian.Peek();
                            this.link_visited[i] = true;            /* assign node sudah dikunjungi  */
                            this.total_linked[i]++;                 /* nEff baris matriks bertambah  */
                        }
                    }
                }
                
                this.antrian.Dequeue();
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

        public string[,] get_mutual_friends()
        {
            return this.mutual_friends;
        }

    }
}
