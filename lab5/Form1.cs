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
using System.Collections;

namespace lab5
{
    public partial class Form1 : Form
    {
        //переменные 
        string text_from_file = "";
        Int32[,] arr_main;
        Int32 count_of_word;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
        }

     
        //методы
        public void method_read_from_file (ref string text_out) // запись текста в строку
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Name_file_to_read = openFileDialog1.FileName;
                using (var R = new StreamReader(Name_file_to_read, Encoding.GetEncoding(1251)))
                {
                    text_out = R.ReadLine();
                }
            }

        }

        public void method_save_to_file(string text_in)// запись текста в файл
        {

                saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                        string Name_file_to_write = saveFileDialog1.FileName;                        
                        using (StreamWriter W = new StreamWriter(Name_file_to_write, true, Encoding.GetEncoding(1251)))
                        {
                            W.WriteLine(text_in);
                        }
                }
        }


        public void method_count_of_words (string text_in,string word_to_find,ref Int32 count)// подсчет слов в тексте
        {
            count = 0;
            text_in.Trim();
            Int32 lengh_word = word_to_find.Length;
            for (int i=0; i<=text_in.Length-lengh_word; i++ )
            {
                string temp_string = text_in.Substring(i, lengh_word);
                if (temp_string==word_to_find)
                {
                    count++;        
                }
                temp_string = "";
            }

        }

        public void method_gen_arr(TextBox size1, TextBox size2, TextBox Value_min, TextBox Value_max, ref int [,] arr_out )//генерация массива данных
        {
            arr_out = new int [Int32.Parse(size1.Text), Int32.Parse(size2.Text)];
            Random rnd = new Random();
            for (int i=0; i< arr_out.GetLength(0);i++)
            {
                for(int j=0;j< arr_out.GetLength(0);j++)
                {
                    arr_out[i, j] = rnd.Next(Int32.Parse(Value_min.Text), Int32.Parse(Value_max.Text));
                }
            }
        }

        public void metthod_arr_to_grid (int [,] arr_in,ref DataGridView out_DataGrid)
        {
            out_DataGrid.RowCount = arr_in.GetLength(0);
            out_DataGrid.ColumnCount = arr_in.GetLength(0);
            for (int i=0;i<arr_in.GetLength(0);i++)
            {
                for(int j=0;j< arr_in.GetLength(1);j++)
                {
                    out_DataGrid.Rows[i].Cells[j].Value = String.Format("{0}", arr_in[i,j]);
                }

            }



        }
        


        //форма
        private void button1_Click(object sender, EventArgs e)// вывод текста в текстбокс
        {
            method_read_from_file(ref text_from_file);
            textBox1.Text = text_from_file;
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            method_count_of_words(text_from_file, textBox2.Text ,ref count_of_word);
            label4.Text = Convert.ToString(count_of_word);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            method_save_to_file(label4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arr_main = new Int32[Int32.Parse(textBox3.Text),Int32.Parse(textBox4.Text)];
            method_gen_arr(textBox3,textBox4,textBox5,textBox6,ref arr_main);
            metthod_arr_to_grid(arr_main,ref dataGridView1);
        }
    }
}
