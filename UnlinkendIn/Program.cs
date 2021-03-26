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
        public bool IsIndexInArray(int[] array, int num)
        {
            return array.Contains(num);
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
    */
}
