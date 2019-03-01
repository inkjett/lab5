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
        Int32[,] arr_with_canges;
        List<List<int>> arr_with_canges_elements;

        Int32 count_of_word;
        Int32 max_value=0;
        Int32 count_of_list;



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

        public void method_arr_to_grid (int [,] arr_in,ref DataGridView out_DataGrid) // метод вывода обычного массива в грид
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

        public void method_arr_list_to_grid(List<List<int>> arr_in, ref DataGridView out_DataGrid) // метод вывода list массива в грид
        {
            Int32 row = arr_in.Count;
           /* foreach (var a in arr_in)
            {
                Int32 i = 0;
                Int32 j = 0;
                for (int i = 0; i < arr_in.Count; i++)
                {
                    for (int j = 0; j < arr_in.Count; j++)
                    {
                        out_DataGrid.Rows.Add(arr_in[i],arr_in[j]);
                    }
                }
            }*/


        }



        public Int32 method_max_value (int [,]arr_in)// метод для определения максимального значения по модулю
        {
            Int32 temp_max_value=0;
            for (int i = 0; i < arr_in.GetLength(0); i++)
                for (int j = 0; j < arr_in.GetLength(1); j++)
                {
                    if (Math.Abs(temp_max_value)==arr_in[i,j])
                    {
                        temp_max_value = arr_in[i, j];
                    }
                }
            return max_value; //= temp_max_value;
        }
            
        public Int32[,] method_change_max(int [,]arr_in, int max_value)//Метод для замены четных значений максимальным значением по модулю
        {
                //arr_out = new int[arr_in.GetLength(0), arr_in.GetLength(1)];
                for (int i = 0; i < arr_in.GetLength(0); i++)
                for (int j = 0; j < arr_in.GetLength(1); j++)
                {
                   if (arr_in[i,j]%2==0)
                    {
                        arr_in[i, j] = max_value;
                    }
                  }
            return arr_in;
        }

        public void arr_change_line(int[,] arr_in, ref List<List<int>> arr_out ) // метод выведения местросполжения несимитричных элементов отностиельно вертикальной оси.
        {
            // добавление столбца GetLength(1) - столбцы, GetLength(0) - стороки 
            // i - столбец  j - строка
            //Int32 count_for_colum = 0;
            Int32 temp_arr_number;
            Int32 Middel_line = 0;
            arr_out = new List<List<int>>();
            List<int> row = new List<int>();
            //List<List<int>> colum = new List<List<int>>();
            if (arr_in.GetLength(0) % 2 != 0)//опрделение строки четные или нет
            {
                Middel_line = (arr_in.GetLength(0) / 2);         //определение средней строки       
                for (int i = 0; i < arr_in.GetLength(0); i++)
                {
                    temp_arr_number = arr_in[0, i];
                    arr_in[0, i] = arr_in[Middel_line, i];
                    arr_in[Middel_line, i] = temp_arr_number;
                }

            }
            for (int j = 0; j < arr_in.GetLength(1); j++)
            {
                if (j < Middel_line)
                {
                    Int32 left_count_j = 0;
                    Int32 right_count_j = arr_in.GetLength(0) - 1;
                    row = new List<int>();
                    for (int i = 0; i < arr_in.GetLength(1); i++)
                    {

                        if (arr_in[i, j + left_count_j] != arr_in[i, j + right_count_j])
                        {
                            arr_out.Add(row);// добавляет новый ряд в массив                        
                            for (int ii = 0; ii <= 4; ii++)
                            { arr_out[0].Add(0); }// добавляем столбецы                        
                            arr_out[i][0] = i; // записываем в таблицу координаты элементов
                            arr_out[i][1] = j;
                            arr_out[i][3] = i;
                            arr_out[i][4] = j + right_count_j;
                            left_count_j++;
                            right_count_j--;
                            count_of_list++;
                        }

                    }
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
            method_arr_to_grid(arr_main,ref dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {            
            max_value = method_max_value(arr_main);
            arr_with_canges = method_change_max(arr_main,max_value);
            arr_change_line(arr_main,ref arr_with_canges_elements);
            //method_arr_to_grid(arr_with_canges_elements, ref dataGridView1);
            Int32 row = arr_with_canges_elements.Count;

        }
    }
}
