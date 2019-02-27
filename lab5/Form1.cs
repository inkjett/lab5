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

namespace lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //переменные 
        string text_from_file = "";
        Int32 count_of_word;
        //методы
        public void method_read_from_file (ref string text_out) // запись текста в строку
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Name_file = openFileDialog1.FileName;
                var R = new StreamReader(Name_file, Encoding.GetEncoding(1251));
                text_out = R.ReadLine();
            }

        }


        public void method_count_of_words (string text_in,string word_to_find,ref Int32 count)
        {
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


        //кнопки формы
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
    }
}
