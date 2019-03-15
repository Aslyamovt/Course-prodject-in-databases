using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Система_учёта_заказов_для_завода_РИЗ
{
    public partial class Form2 : Form
    {
        public class Functional
        {
            public static void Read_table(int c_colomn, string r_data, DataGridView dataGridView1, SqlConnection con)
            {
                SqlCommand cmd = new SqlCommand(r_data, con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string[]> data = new List<string[]>();

                for (int i = 0; i < c_colomn; i++)
                {
                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.HeaderText = reader.GetName(i);
                    dataGridView1.Columns.Add(dgvc);
                }

                while (reader.Read())
                {
                    data.Add(new string[c_colomn]);

                    for (int i = 0; i < c_colomn; i++)
                        data[data.Count - 1][i] = reader[i].ToString();
                }

                reader.Close();

                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }


            public static void Read_table(string c_colomn, string r_data, DataGridView dataGridView1, SqlConnection con)
            {
                SqlCommand cmd = new SqlCommand(c_colomn, con);
                int k = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand(r_data, con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string[]> data = new List<string[]>();

                for (int i = 0; i < k; i++)
                {
                    DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                    dgvc.HeaderText = reader.GetName(i);
                    dataGridView1.Columns.Add(dgvc);
                }

                while (reader.Read())
                {
                    data.Add(new string[k]);

                    for (int i = 0; i < k; i++)
                        data[data.Count - 1][i] = reader[i].ToString();
                }

                reader.Close();

                foreach (string[] s in data)
                    dataGridView1.Rows.Add(s);
            }

            public static void Read_okno(string r_data,SqlConnection con, ComboBox comboBox1)
            {
                SqlCommand cmd = new SqlCommand(r_data, con);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> data = new List<string>();

                while (reader.Read())
                    data.Add(reader[0].ToString());

                reader.Close();

                foreach (string s in data)
                    comboBox1.Items.Add(s);
            }

            public static void Read_table_users(DataGridView dataGridView1, List<string[]> L, ComboBox comboBox1)
            {
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.HeaderText = "Имя пользователя";
                dataGridView1.Columns.Add(dgvc);
                dgvc = new DataGridViewTextBoxColumn();
                dgvc.HeaderText = "Пароль пользователя";
                dataGridView1.Columns.Add(dgvc);
                for(int i=0; i< L.Count; i++)
                { 
                    comboBox1.Items.Add(L[i][0]);
                    dataGridView1.Rows.Add(L[i]);
                }
            }
        }




        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 form, int m)
        {
            InitializeComponent();
            avtwin = form;
            mandat = m;
        }

        Form1 avtwin;
        int mandat;
        string zav;

        SqlConnection conn;
        bool kn = false;
        bool zvs = false;
        int k;

        public List<string> sluz = new List<string>();


        private void Form2_Load(object sender, EventArgs e)
        {
            if (mandat == 1)
            {
                label3.Visible = true;
                comboBox1.Visible = true;
                button3.Visible = true;
                label2.Visible = false;
                comboBox3.Visible = false;
                label1.Text = "Выберите пользователя";
                button1.Text = "Добавить пользователя";
                button2.Text = "Удалить пользователя";
                button3.Text = "Изменить информацию о пользователе";
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Список системных администраторов");
                comboBox2.Items.Add("Список старших мастеров");
                comboBox2.Items.Add("Список менеджеро по закупкам");
                comboBox2.Items.Add("Список управленцев");
                comboBox2.Text = "Список системных администраторов";
                comboBox2.SelectedIndex = 0;
                MessageBox.Show("Вхон успешно произведён\n\nВаш мандат доступа - Системный администратор");
            }
            else
            {

                string connStr = @"Data Source=DEXP\SQLEXPRESS;
                            Initial Catalog=Заказы_инструметнов_на_РИЗе;
                            User ID = Areyouprogram;
                            Password = yes";

                conn = new SqlConnection(connStr);
                try
                {
                    conn.Open();
                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.Message);
                    avtwin.Visible = true;
                    this.Close();
                }

                //MessageBox.Show("Соединение успешно произведено");

                if (mandat == 2)
                {
                    MessageBox.Show("Соединение и вход успешно произведены\n\nВаш мандат доступа - старший мастер");
                    label3.Visible = true;
                    button3.Visible = false;
                    comboBox2.Visible = true;
                    label2.Visible = false;
                    comboBox3.Visible = false;
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Список доступных инструментов");
                    comboBox2.Items.Add("Список заказов выбранного завода");
                    comboBox2.Text = "Список доступных инструментов";
                    comboBox2.SelectedIndex = 0;
                    button1.Visible = false;
                    button2.Visible = false;
                    label1.Visible = false;
                }
                else if (mandat == 3)
                {
                    MessageBox.Show("Соединение и вход успешно произведены\n\nВаш мандат доступа - менеджер по продажам");
                    label3.Visible = true;
                    button3.Visible = false;
                    comboBox2.Visible = true;
                    label2.Visible = false;
                    comboBox3.Visible = false;
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Список доступных инструментов");
                    comboBox2.Items.Add("Список заказов");
                    comboBox2.Items.Add("Список заказов без поставщиков");
                    comboBox2.Items.Add("Список поставщиков");
                    comboBox2.Text = "Список доступных инструментов";
                    comboBox2.SelectedIndex = 0;
                    button1.Text = "Добавить инструмент";
                    button2.Text = "Удалить инструмент";
                    label1.Text = "Выберите инструмент";
                }
                else if (mandat == 4)
                {
                    MessageBox.Show("Соединение и вход успешно произведены\n\nВаш мандат доступа - управленец");
                    label3.Visible = true;
                    comboBox1.Visible = true;
                    button3.Visible = true;
                    label2.Visible = false;
                    comboBox3.Visible = false;
                    comboBox2.Visible = false;
                    button1.Text = "Добавить завод";
                    button2.Text = "Удалить завод";
                    button3.Text = "Изменить информацию о заводе";
                    label1.Text = "Выберите завод";
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox1.Items.Clear();
                    Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Заводы_заказчики';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]", dataGridView1, conn);
                    Functional.Read_okno("SELECT [Шифр_завода] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]", conn, comboBox1);
                }
            }
        }


        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(mandat>1)
                conn.Close();
            if(!kn)
                avtwin.Close();
            kn = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if((mandat == 3) && (comboBox2.SelectedIndex == 0))
            {
                Form3 f = new Form3(3, conn, dataGridView1, "", comboBox1,comboBox3);
                f.Owner = this;
                f.Show();
            }
            else if((mandat == 2) && (comboBox2.SelectedIndex == 1))
            {
                Form3 f = new Form3(4, conn, dataGridView1, "", comboBox1,comboBox3);
                f.Owner = this;
                f.Show();
            }
            else if((mandat==3) && (comboBox2.SelectedIndex == 1))
            {
                Form3 f = new Form3(5, conn, dataGridView1, "", comboBox1, comboBox3);
                f.Owner = this;
                f.Show();
            }
            else if((mandat == 3) && (comboBox2.SelectedIndex == 2))
            {
                Form3 f = new Form3(6, conn, dataGridView1, "", comboBox1, comboBox3);
                f.Owner = this;
                f.Show();
            }
            else if ((mandat == 3) && (comboBox2.SelectedIndex == 3))
            {
                Form3 f = new Form3(7, conn, dataGridView1, "", comboBox1, comboBox3);
                f.Owner = this;
                f.Show();
            }
            else if(mandat == 1)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    Form3 f = new Form3(9, dataGridView1, comboBox1, avtwin);
                    f.Owner = this;
                    f.Show();
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    Form3 f = new Form3(10, dataGridView1, comboBox1, avtwin);
                    f.Owner = this;
                    f.Show();
                }

                else if (comboBox2.SelectedIndex == 2)
                {
                    Form3 f = new Form3(11, dataGridView1, comboBox1, avtwin);
                    f.Owner = this;
                    f.Show();
                }

                else
                {
                    Form3 f = new Form3(12, dataGridView1, comboBox1, avtwin);
                    f.Owner = this;
                    f.Show();
                }
            }
            else
            {
                Form3 f = new Form3(1, conn, dataGridView1, "", comboBox1,comboBox3);
                f.Owner = this;
                f.Show();
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            zav = comboBox1.Text;
            if (zvs)
            {
                zav = null;
            }

            if ((mandat == 2) && (comboBox2.SelectedIndex == 1))
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                string sql = string.Format("select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа], [Инструмент], [Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] = [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа] where[Шифр_завода]='{0}'", zav);
                Functional.Read_table(3, sql, dataGridView1, conn);
            }
            zvs = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (zav == null)
            {
                MessageBox.Show("Ошибка! Не выбран элемент из поля выше");
            }
            else
            {
                if ((mandat == 3) && (comboBox2.SelectedIndex == 0))
                {
                    string sql1 = string.Format("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]  where [№_заказа] in(select [№_заказа] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] where [Инструмент] ='{0}')", comboBox1.Text);
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();
                    string sql2 = string.Format("Delete from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] where [Инструмент] ='{0}'", comboBox1.Text);
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.ExecuteNonQuery();
                    string sql = string.Format("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты]  where [Инструмент] = '{0}'", comboBox1.Text);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if ((string)dr.Cells[0].Value == zav)
                        {
                            comboBox1.Items.Remove(dr.Cells[0].Value);
                            dataGridView1.Rows.RemoveAt(dr.Index);
                            break;
                        }
                    }
                    zav = null;
                }
                else if(((mandat == 2) && (comboBox2.SelectedIndex == 1)) || ((mandat == 3)&&((comboBox2.SelectedIndex == 1)|| (comboBox2.SelectedIndex == 2))))
                {
                    string sql = string.Format("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах]  where [№_заказа] = '{0}'", comboBox3.Text);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    string sql1 = string.Format("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]  where [№_заказа] = '{0}'", comboBox3.Text);
                    cmd = new SqlCommand(sql1, conn);
                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox3.Items.Clear();
                    string sql2 = string.Format("select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа], [Инструмент], [Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join[Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] =[Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа] where[Шифр_завода]='{0}'", comboBox1.Text);
                    Functional.Read_table(3, sql2, dataGridView1, conn);
                    if (!(comboBox2.SelectedIndex == 2))
                        Functional.Read_okno("SELECT [№_заказа] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]", conn, comboBox1);
                    else
                        Functional.Read_okno("SELECT [№_заказа] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Поставщик] is null", conn, comboBox1);
                    if (mandat == 3)
                    {
                        SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Поставщик] is null", conn);
                        int k = Convert.ToInt32(cmd1.ExecuteScalar());
                        string label = string.Format("Число необработанных заказов: {0}", k);
                        label1.Text = label;
                    }
                } 
                else if((mandat == 3) && (comboBox2.SelectedIndex == 3))
                {
                    string sql = string.Format("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]  where [Поставщик] = '{0}'", comboBox1.Text);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    string sql1 = string.Format("Update [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] set [Поставщик] = NUUL where [Поставщик] = '{0}'", comboBox1.Text);
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox3.Items.Clear();
                    Functional.Read_okno("SELECT [Поставщик] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", conn, comboBox1);
                    Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", dataGridView1, conn);
                }
                else if (mandat == 1)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        avtwin.admins.RemoveAt(comboBox1.SelectedIndex);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Functional.Read_table_users(dataGridView1, avtwin.admins, comboBox1);
                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        avtwin.stmasters.RemoveAt(comboBox1.SelectedIndex);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Functional.Read_table_users(dataGridView1, avtwin.stmasters, comboBox1);
                    }
                    else if (comboBox2.SelectedIndex == 2)
                    {
                        avtwin.managers.RemoveAt(comboBox1.SelectedIndex);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Functional.Read_table_users(dataGridView1, avtwin.managers, comboBox1);
                    }
                    else
                    {
                        avtwin.upravlenz.RemoveAt(comboBox1.SelectedIndex);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Functional.Read_table_users(dataGridView1, avtwin.upravlenz, comboBox1);
                    }
                }
                else
                {
                    string sql3 = string.Format("Delete from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] where [№_заказа] in (select [№_заказа] from [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Шифр_завода] = '{0}')",zav);
                    SqlCommand cmd2 = new SqlCommand(sql3, conn);
                    cmd2.ExecuteNonQuery();
                    string sql4 = string.Format("Delete from [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Шифр_завода] = '{0}'", zav);
                    SqlCommand cmd3 = new SqlCommand(sql4, conn);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd = new SqlCommand("Delete From [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]  where [Шифр_завода] = @Шифр_завода", conn);
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Шифр_завода";
                    param.Value = zav;
                    param.SqlDbType = SqlDbType.NChar;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {

                        if ((string)dr.Cells[0].Value == zav)
                        {
                            comboBox1.Items.Remove(dr.Cells[0].Value);
                            dataGridView1.Rows.RemoveAt(dr.Index);
                            break;
                        }
                    }
                    zav = null;
                }

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (zav == null)
            {
                MessageBox.Show("Ошибка! Не выбран завод");
            }
            else
            {
                if (mandat == 3)
                {
                    Form3 f = new Form3(8, conn, dataGridView1, zav, comboBox1, comboBox3);
                    f.Owner = this;
                    f.Show();
                }
                else if (mandat == 1)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        Form3 f = new Form3(13, dataGridView1, comboBox1, avtwin);
                        f.Owner = this;
                        f.Show();
                    }
                    else if (comboBox2.SelectedIndex == 1)
                    {
                        Form3 f = new Form3(14, dataGridView1, comboBox1, avtwin);
                        f.Owner = this;
                        f.Show();
                    }

                    else if (comboBox2.SelectedIndex == 2)
                    {
                        Form3 f = new Form3(15, dataGridView1, comboBox1, avtwin);
                        f.Owner = this;
                        f.Show();
                    }

                    else
                    {
                        Form3 f = new Form3(16, dataGridView1, comboBox1, avtwin);
                        f.Owner = this;
                        f.Show();
                    }
                }
                else
                {
                    Form3 f = new Form3(2, conn, dataGridView1, zav, comboBox1, comboBox3);
                    f.Owner = this;
                    f.Show();
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            kn = true;
            avtwin.Visible = true;
            this.Close();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((mandat == 2) && (comboBox2.SelectedIndex == 0))
            {
                zvs = true;
                label3.Visible = false;
                comboBox1.Visible = false;
                button3.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                comboBox3.Visible = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Инструменты';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты]", dataGridView1, conn);
            }
            else if ((mandat == 3) && (comboBox2.SelectedIndex == 0))
            {
                zvs = true;
                label3.Visible = true;
                comboBox1.Visible = true;
                button3.Visible = false;
                comboBox1.Text = "";
                button1.Text = "Добавить инструмент";
                button2.Text = "Удалить инструмент";
                label1.Text = "Выберите инструмент";
                label2.Visible = false;
                comboBox3.Visible = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                comboBox1.Items.Clear();
                Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Инструменты';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты]", dataGridView1, conn);
                Functional.Read_okno("SELECT [Инструмент] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты]", conn, comboBox1);
            }
            else if ((mandat == 2) && (comboBox2.SelectedIndex == 1))
            {
                zvs = false;
                label3.Visible = true;
                comboBox1.Visible = true;
                label2.Visible = true;
                comboBox3.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                label1.Visible = true;
                button1.Text = "Разместить заказ";
                button2.Text = "Удалить заказ";
                label1.Text = "Выберите завод";
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                Functional.Read_okno("SELECT [№_заказа] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]", conn, comboBox3);
                Functional.Read_okno("SELECT [Шифр_завода] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]", conn, comboBox1);
                comboBox1.Text = "АВЗ";
                comboBox1.SelectedIndex = 0;
            }
            else if ((mandat == 3) && (comboBox2.SelectedIndex == 1))
            {
                zvs = false;
                label3.Visible = true;
                label2.Visible = true;
                comboBox1.Visible = false;
                comboBox3.Visible = true;
                button3.Visible = false;
                button1.Text = "Изменить информацию о поставщике в заказе";
                button2.Text = "Удалить заказ";
                comboBox3.Items.Clear();
                Functional.Read_okno("SELECT [№_заказа] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]", conn, comboBox3);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Поставщик] is null", conn);
                int k = Convert.ToInt32(cmd.ExecuteScalar());
                string label = string.Format("Число необработанных заказов: {0}", k);
                label1.Text = label;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                //Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики_в_заказах_заводов';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]", dataGridView1, conn);
                Functional.Read_table(5, "select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа],[Шифр_завода],[Поставщик], [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[Инструмент],[Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] = [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа]", dataGridView1, conn);
            }
            else if((mandat == 3) && (comboBox2.SelectedIndex == 2))
            {
                zvs = false;
                label3.Visible = true;
                label2.Visible = true;
                comboBox1.Visible = false;
                comboBox3.Visible = true;
                button3.Visible = false;
                button1.Text = "Изменить информацию о поставщике в заказе";
                button2.Text = "Удалить заказ";
                comboBox3.Items.Clear();
                Functional.Read_okno("SELECT [№_заказа] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] Where [Поставщик] is null", conn, comboBox3);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] where [Поставщик] is null", conn);
                k = Convert.ToInt32(cmd.ExecuteScalar());
                string label = string.Format("Число необработанных заказов: {0}", k);
                label1.Text = label;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                //Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики_в_заказах_заводов';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] WHERE [Поставщик] is null", dataGridView1, conn);
                Functional.Read_table(5, "select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа],[Шифр_завода],[Поставщик], [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[Инструмент],[Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] = [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа] WHERE [Поставщик] is null", dataGridView1, conn);
            }
            else if ((mandat == 3) && (comboBox2.SelectedIndex == 3))
            {
                zvs = false;
                label3.Visible = true;
                label2.Visible = false;
                comboBox1.Visible = true;
                comboBox3.Visible = false;
                button3.Visible = true;
                button1.Text = "Добавить поставщика";
                button2.Text = "Удалить поставщика";
                button3.Text = "Изменить информацию о поставщике";
                comboBox1.Items.Clear();
                Functional.Read_okno("SELECT [Поставщик] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", conn, comboBox1);
                label1.Text = "Выберите поставщика";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", dataGridView1, conn);
            }
            else
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox1.Items.Clear();
                    Functional.Read_table_users(dataGridView1, avtwin.admins, comboBox1);
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox1.Items.Clear();
                    Functional.Read_table_users(dataGridView1, avtwin.stmasters, comboBox1);
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox1.Items.Clear();
                    Functional.Read_table_users(dataGridView1, avtwin.managers, comboBox1);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    comboBox1.Items.Clear();
                    Functional.Read_table_users(dataGridView1, avtwin.upravlenz, comboBox1);
                }
            }
        }
    }
}
