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

namespace Система_учёта_заказов_для_завода_РИЗ
{
    public partial class Form1 : Form
    {
        class Functional
        {
            static public void ReadArray(object users, string adr)
            {
                using (StreamReader sr = new StreamReader(adr))
                {
                    var vsusers = users as List<string[]>;
                    string temp = sr.ReadLine();
                    string[] line = temp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < line.Length; j+=2)
                    {
                        string[] sl = new string[2];
                        sl[0] = line[j];
                        sl[1] = line[j + 1];
                        vsusers.Add(sl);
                    }
                }
            }

            static public void WriteArray(List<string[]> users, string adr)
            {
                using (StreamWriter sw = new StreamWriter(adr, false))
                {
                    int k = users.Count;
                    string[] line = new string[2 * k];
                    for (int i = 0; i < k; i++)
                    {
                        line[2 * i] = users[i][0];
                        line[2 * i + 1] = users[i][1];
                    }

                    //Метод Join() склеивает элементы массива line в одну строку, разделяя их пробелами
                    sw.WriteLine(String.Join(" ", line));
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        public List<string[]> admins = new List<string[]>();
        public List<string[]> stmasters = new List<string[]>();
        public List<string[]> managers = new List<string[]>();
        public List<string[]> upravlenz = new List<string[]>();

        private void button1_Click(object sender, EventArgs e)
        {
            bool pr = false;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Не введён идентификатор пользователя!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Не введён пароль пользователя!");
            }
            else
            {
                string[] sl = { textBox1.Text, textBox2.Text };
                for(int i=0;i<admins.Count;i++)
                    if (admins[i][0]==textBox1.Text&& admins[i][1] == textBox2.Text)
                    {
                        this.Visible = false;
                        Form2 fr = new Form2(this, 1);
                        fr.Show();
                        pr = true;
                        break;
                    }
                if (!pr)
                {
                    for (int i = 0; i < stmasters.Count; i++)
                        if (stmasters[i][0] == textBox1.Text && stmasters[i][1] == textBox2.Text)
                        {
                            this.Visible = false;
                            Form2 fr = new Form2(this, 2);
                            fr.Show();
                            pr = true;
                            break;
                        }
                }
                if (!pr)
                {
                    for (int i = 0; i < managers.Count; i++)
                        if (managers[i][0] == textBox1.Text && managers[i][1] == textBox2.Text)
                        {
                            this.Visible = false;
                            Form2 fr = new Form2(this, 3);
                            fr.Show();
                            pr = true;
                            break;
                        }
                }
                if (!pr)
                {
                    for (int i = 0; i < upravlenz.Count; i++)
                        if (upravlenz[i][0] == textBox1.Text && upravlenz[i][1] == textBox2.Text)
                        {
                            this.Visible = false;
                            Form2 fr = new Form2(this, 4);
                            fr.Show();
                            pr = true;
                            break;
                        }
                }
                if(!pr)
                {
                    MessageBox.Show("Ошибка входа: неверное имя пользователя или пароль");
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }     
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*string[] d = { "Lerax", "timur" };
            admins.Add(d);
            string[] a = { "Пётр","Петров"};
            stmasters.Add(a);
            string[] b = {"Иван","Иванов"};
            managers.Add(b);
            string[] c = {"Алексей","Алексеев"};
            upravlenz.Add(c);*/
            Functional.WriteArray(admins, "admins.dat");
            Functional.WriteArray(stmasters, "stmasters.dat");
            Functional.WriteArray(managers, "managers.dat");
            Functional.WriteArray(upravlenz, "upravlenz.dat");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Functional.ReadArray((object)admins, "admins.dat");
            Functional.ReadArray((object)stmasters, "stmasters.dat");
            Functional.ReadArray((object)managers, "managers.dat");
            Functional.ReadArray((object)upravlenz, "upravlenz.dat");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }
    }
}
