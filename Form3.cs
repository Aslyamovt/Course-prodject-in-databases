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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int i, DataGridView dgv, ComboBox cmb, Form1 f1)
        {
            InitializeComponent();
            opr = i;
            f = this.Owner as Form2;
            ff = f1;
            dataGridView1 = dgv;
            comboBox1 = cmb;
            textBox3.Visible = false;
            textBox4.Visible = false;
            comboBox2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            this.Width = 341;
            button1.Location = new Point(102,68);
            label1.Text = "Введите логин";
            label2.Text = "Введите пароль";
            switch (i)
            { 
                case (9):
                case (10):
                case (11):
                case (12):
                    button1.Text = "Ок";
                    break;
                case (13):
                    button1.Text = "Сохранить изменения";
                    textBox1.Text = ff.admins[cmb.SelectedIndex][0];
                    textBox2.Text = ff.admins[cmb.SelectedIndex][1];
                    break;
                case (14):
                    button1.Text = "Сохранить изменения";
                    textBox1.Text = ff.stmasters[cmb.SelectedIndex][0];
                    textBox2.Text = ff.stmasters[cmb.SelectedIndex][1];
                    break;
                case (15):
                    button1.Text = "Сохранить изменения";
                    textBox1.Text = ff.managers[cmb.SelectedIndex][0];
                    textBox2.Text = ff.managers[cmb.SelectedIndex][1];
                    break;
                case (16):
                    button1.Text = "Сохранить изменения";
                    textBox1.Text = ff.upravlenz[cmb.SelectedIndex][0];
                    textBox2.Text = ff.upravlenz[cmb.SelectedIndex][1];
                    break;
            }
        }

        public Form3(int i, SqlConnection con, DataGridView dgv,string zav, ComboBox cmb,ComboBox cmb1)
        {
            InitializeComponent();
            opr = i;
            f = this.Owner as Form2;
            conn = con;
            dataGridView1 = dgv;
            zavod = zav.TrimEnd(' ');
            comboBox1 = cmb;
            cb = cmb1;
            switch (i)
            {
                case (1):
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = false;
                    label4.Visible = false;
                    label1.Text = "Введите шифр завода";
                    label2.Text = "Введите название завода";
                    label3.Text = "Ввведите код завода";
                    comboBox2.Visible = false;
                    this.Width = 491;
                    button1.Location = new Point(182, 68);
                    button1.Text = "Ок";
                    break;
                case (2):
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    label4.Visible = false;
                    textBox4.Visible = false;
                    comboBox2.Visible = false;
                    this.Width = 491;
                    button1.Location = new Point(182, 68);
                    label1.Text = "Введите шифр завода";
                    label2.Text = "Введите название завода";
                    label3.Text = "Ввведите код завода";
                    button1.Text = "Сохранить изменения";
                    foreach (DataGridViewRow dr in dataGridView1.Rows)

                        if ((string)dr.Cells[0].Value == zav)
                        {
                            textBox1.Text = (string)dr.Cells[0].Value;
                            textBox2.Text = (string)dr.Cells[1].Value;
                            textBox3.Text = (string)dr.Cells[2].Value;
                            break;
                        }
                    break;
                case (3):
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = true;
                    comboBox2.Visible = false;
                    label1.Text = "Введите название инструмента";
                    label2.Text = "Введите код инструмента";
                    label3.Text = "Ввведите единицы измерения";
                    label4.Text = "Ввведите цену в рублях";
                    button1.Text = "Ок";
                    break;
                case (4):
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = true;
                    textBox4.Visible = false;
                    comboBox2.Visible = true;
                    label1.Visible = false;
                    label4.Visible = false;
                    label2.Text = "Выберите инструмент";
                    label3.Text = "Ввведите колличество";
                    button1.Text = "Ок";
                    Form2.Functional.Read_okno("SELECT [Инструмент] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты]", conn, comboBox2);
                    break;
                case (5):
                case (6):
                    textBox1.Visible = true;
                    textBox1.Text = cmb1.Text;
                    textBox1.Enabled = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    this.Width = 341;
                    button1.Location = new Point(102, 68);
                    label1.Text = "Номер заказа";
                    label2.Text = "Выберите поставщика";
                    button1.Text = "Ок";
                    comboBox2.Items.Add("NULL");
                    Form2.Functional.Read_okno("SELECT [Поставщик] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", conn, comboBox2);
                    comboBox2.Text = "NULL";
                    comboBox2.SelectedIndex = 0;
                    break;
                case (7):
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = false;
                    comboBox2.Visible = false;
                    label4.Visible = false;
                    this.Width = 491;
                    button1.Location = new Point(182, 68);
                    label1.Text = "Введите наименование поставщика";
                    label2.Text = "Введите ИНН";
                    label3.Text = "Ввведите адрес";
                    button1.Text = "Ок";
                    break;
                case (8):
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox4.Visible = false;
                    comboBox2.Visible = false;
                    label4.Visible = false;
                    this.Width = 491;
                    button1.Location = new Point(182, 68);
                    label1.Text = "Введите наименование поставщика";
                    label2.Text = "Введите ИНН";
                    label3.Text = "Ввведите адрес";
                    button1.Text = "Сохранить изменения";
                    foreach (DataGridViewRow dr in dataGridView1.Rows)

                        if ((string)dr.Cells[0].Value == zav)
                        {
                            textBox1.Text = (string)dr.Cells[0].Value;
                            textBox2.Text = (string)dr.Cells[1].Value;
                            textBox3.Text = (string)dr.Cells[2].Value;
                            break;
                        }
                    break;
            }
        }

        string zavod;
        int opr;
        Form1 ff;
        Form2 f;
        SqlConnection conn;
        DataGridView dataGridView1;
        ComboBox comboBox1;
        ComboBox cb;
        string sqlfor5;
        List<string[]> Li;

        string inst;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (opr)
                {
                    case (1):
                        string[] zav = { textBox1.Text, textBox2.Text, textBox3.Text };
                        dataGridView1.Rows.Add(zav);
                        SqlCommand cmd = new SqlCommand("Insert into [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики] ([Шифр_завода],[Название_завода_заказчика],[Код_завода]) Values (@Шифр_завода,@Название_завода_заказчика,@Код_завода)", conn);
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@Шифр_завода";
                        param.Value = zav[0];
                        param.SqlDbType = SqlDbType.NChar;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter();
                        param.ParameterName = "@Название_завода_заказчика";
                        param.Value = zav[1];
                        param.SqlDbType = SqlDbType.NChar;
                        cmd.Parameters.Add(param);
                        param = new SqlParameter();
                        param.ParameterName = "@Код_завода";
                        param.Value = Convert.ToInt32(zav[2]);
                        param.SqlDbType = SqlDbType.Int;
                        cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        comboBox1.Items.Add(zav[0]);
                        this.Close();
                        break;
                    case (2):
                        string sql = string.Format("Update [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики] set [Шифр_завода] = '{0}', [Название_завода_заказчика] = '{1}',[Код_завода] = '{2}' where [Шифр_завода] = '{3}'",textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text),zavod);
                        SqlCommand cmd1 = new SqlCommand(sql, conn);
                        cmd1.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Заводы_заказчики';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]", dataGridView1, conn);
                        Form2.Functional.Read_okno("SELECT [Шифр_завода] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Заводы_заказчики]", conn, comboBox1);
                        this.Close();
                        break;
                    case (3):
                        string[] zav2 = { textBox1.Text, textBox2.Text, textBox3.Text,textBox4.Text };
                        dataGridView1.Rows.Add(zav2);
                        string sql2 = string.Format("Insert into [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты] ([Инструмент],[Код_инструмента],[Единицы_измерения],[Цена_в_рублях]) Values ('{0}','{1}','{2}','{3}')", zav2[0], Convert.ToInt32(zav2[1]), zav2[2], Convert.ToInt32(zav2[3]));
                        SqlCommand cmd2 = new SqlCommand(sql2, conn);
                        cmd2.ExecuteNonQuery();
                        comboBox1.Items.Add(zav2[0]);
                        this.Close();
                        break;
                    case (4):
                        if (inst == null)
                        {
                            MessageBox.Show("Ошибка! Не выбран инструмент");
                        }
                        else
                        {
                            string sql3 = string.Format("SELECT MAX([№_заказа]) FROM[Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]");
                            SqlCommand cmd3 = new SqlCommand(sql3, conn);
                            int k = Convert.ToInt32(cmd3.ExecuteScalar());
                            sql3 = string.Format("Insert into [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] ([Шифр_завода],[№_заказа]) Values ('{0}','{1}')",comboBox1.Text, k+1);
                            cmd3 = new SqlCommand(sql3, conn);
                            cmd3.ExecuteNonQuery();
                            string sql4 = string.Format("Insert into [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] ([№_заказа],[Инструмент],[Колличество]) Values ('{0}','{1}','{2}')", k+1, comboBox2.Text, Convert.ToInt32(textBox3.Text));
                            cmd3 = new SqlCommand(sql4, conn);
                            cmd3.ExecuteNonQuery();
                            dataGridView1.Rows.Clear();
                            dataGridView1.Columns.Clear();
                            string sql5 = string.Format("select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа], [Инструмент], [Колличество] from[Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join[Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] =[Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа] where[Шифр_завода]='{0}'", comboBox1.Text);
                            Form2.Functional.Read_table(3, sql5, dataGridView1, conn);
                            cb.Items.Add(k + 1);
                            this.Close();
                        }
                        break;
                    case (5):
                        SqlCommand cmd4 = new SqlCommand(sqlfor5, conn);
                        cmd4.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        // Form2.Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики_в_заказах_заводов';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов]", dataGridView1, conn);
                        Form2.Functional.Read_table(5, "select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа],[Шифр_завода],[Поставщик], [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[Инструмент],[Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] = [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа]", dataGridView1, conn);
                        this.Close();
                        break;
                    case (6):
                        SqlCommand cmd5 = new SqlCommand(sqlfor5, conn);
                        cmd5.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        //Form2.Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики_в_заказах_заводов';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] WHERE [Поставщик] is null", dataGridView1, conn);
                        Form2.Functional.Read_table(5, "select [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа],[Шифр_завода],[Поставщик], [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[Инструмент],[Колличество] from [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах] join [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] on [Заказы_инструметнов_на_РИЗе].[dbo].[Инструменты_в_заказах].[№_заказа] = [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов].[№_заказа] WHERE [Поставщик] is null", dataGridView1, conn);
                        this.Close();
                        break;
                    case (7):
                        string[] post = { textBox1.Text, textBox2.Text, textBox3.Text};
                        dataGridView1.Rows.Add(post);
                        string sql6 = string.Format("Insert into [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики] ([Поставщик],[ИНН],[Адрес]) Values ('{0}','{1}','{2}')", post[0], post[1], post[2]);
                        SqlCommand cmd6 = new SqlCommand(sql6, conn);
                        cmd6.ExecuteNonQuery();
                        comboBox1.Items.Add(post[0]);
                        this.Close();
                        break;
                    case (8):
                        string sql7 = string.Format("Update [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики] set [Поставщик] = '{0}', [ИНН] = '{1}',[Адрес] = '{2}' where [Поставщик] = '{3}'", textBox1.Text, textBox2.Text, textBox3.Text, zavod);
                        SqlCommand cmd7 = new SqlCommand(sql7, conn);
                        cmd7.ExecuteNonQuery();
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_NAME = 'Поставщики';", "SELECT * FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", dataGridView1, conn);
                        Form2.Functional.Read_okno("SELECT [Поставщик] FROM [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики]", conn, comboBox1);
                        this.Close();
                        break;
                    case (9):
                        string[] data = {textBox1.Text, textBox2.Text};
                        ff.admins.Add(data);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.admins,comboBox1);
                        this.Close();
                        break;
                    case (10):
                        string[] data1 = { textBox1.Text, textBox2.Text };
                        ff.stmasters.Add(data1);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.stmasters, comboBox1);
                        this.Close();
                        break;
                    case (11):
                        string[] data2 = { textBox1.Text, textBox2.Text };
                        ff.managers.Add(data2);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.managers, comboBox1);
                        this.Close();
                        break;
                    case (12):
                        string[] data3 = { textBox1.Text, textBox2.Text };
                        ff.upravlenz.Add(data3);
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.upravlenz, comboBox1);
                        this.Close();
                        break;
                    case (13):
                        ff.admins[comboBox1.SelectedIndex][0] = textBox1.Text;
                        ff.admins[comboBox1.SelectedIndex][1] = textBox2.Text;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.admins, comboBox1);
                        this.Close();
                        break;
                    case (14):
                        ff.stmasters[comboBox1.SelectedIndex][0] = textBox1.Text;
                        ff.stmasters[comboBox1.SelectedIndex][1] = textBox2.Text;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.stmasters, comboBox1);
                        this.Close();
                        break;
                    case (15):
                        ff.managers[comboBox1.SelectedIndex][0] = textBox1.Text;
                        ff.managers[comboBox1.SelectedIndex][1] = textBox2.Text;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.managers, comboBox1);
                        this.Close();
                        break;
                    case (16):
                        ff.upravlenz[comboBox1.SelectedIndex][0] = textBox1.Text;
                        ff.upravlenz[comboBox1.SelectedIndex][1] = textBox2.Text;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        comboBox1.Items.Clear();
                        Form2.Functional.Read_table_users(dataGridView1, ff.upravlenz, comboBox1);
                        this.Close();
                        break;
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
            
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(opr == 4)
                inst = comboBox2.Text;
            else
            {
                if (comboBox2.SelectedIndex == 0)
                    sqlfor5 = string.Format("UPDATE [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] SET [Поставщик] = NULL WHERE [№_заказа] = '{0}'", Convert.ToInt32(cb.Text));
                else
                    sqlfor5 = string.Format("UPDATE [Заказы_инструметнов_на_РИЗе].[dbo].[Поставщики_в_заказах_заводов] SET [Поставщик] = '{1}' WHERE [№_заказа] = '{0}'", Convert.ToInt32(cb.Text), comboBox2.Text);
            }
        }
    }
}
