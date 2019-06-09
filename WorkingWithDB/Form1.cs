using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MetroFramework.Components;
using MetroFramework.Forms;


namespace WorkingWithDB
{
    
    public partial class Form1 : MetroForm
    {
       SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

       // public static FileInfo fin = new FileInfo(@"WorkingWithDB.exe");
        //public static string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + fin.DirectoryName + "\\Database.mdf";

       private async void Form1_Load(object sender, EventArgs e)
        {
            //  string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //  string path = (System.IO.Path.GetDirectoryName(executable));
            // AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //Change this path "C:\Users\HOME\Desktop\c#\WorkingWithDB\WorkingWithDB\Database.mdf" for your own computer
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=| DataDirectory |\Database.mdf;Integrated Security=True";
            // string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename =| DataDirectory |\Database.mdf; Integrated Security = True; User Instance = True";
            //string connectionString = @"Data Source =.\SQLEXPRESS; Database = myuniquedb; AttachDbFilename =| DataDirectory |\Database.mdf; Integrated Security = True; User Instance = True";
            //qlConnection = new SqlConnection(connectionString);
              string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf; Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

           SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["Name"]) + "       " + Convert.ToString(sqlReader["Sum"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Products] (Id, Name, Sum)VALUES(@Id, @Name, @Sum)", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox8.Text);

                command.Parameters.AddWithValue("Name", textBox1.Text);

                command.Parameters.AddWithValue("Sum", textBox2.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Visible = true;

                label7.Text = "Поля 'Имя' и 'Количество' должны быть заполнены!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["Name"]) + "       " + Convert.ToString(sqlReader["Sum"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Products] SET [Name]=@Name, [Sum]=@Sum WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Name", textBox4.Text);
                command.Parameters.AddWithValue("Sum", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label8.Visible = true;

                label8.Text = "Id должнен быть заполнен!";
            }
            else
            {
                label8.Visible = true;

                label8.Text = "Поля 'Id', 'Имя' и 'Количество' должны быть заполнены!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label8.Visible = true;

                label8.Text = "Id должнен быть заполнен!";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {         
           listBox2.Items.Add(listBox1.SelectedItem); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox1.SelectedItem);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                listBox2.SelectedIndex = i;
                listBox1.Items.Remove(listBox2.SelectedItem);
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("id", listBox2.SelectedItems);
            }

            {
                Stream myStream;
                SaveFileDialog saveFileDialog = new SaveFileDialog();




                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)

                    {


                        StreamWriter sw = new StreamWriter(myStream);

                        sw.WriteLine("РАСПОРЯЖЕНИЕ");
                        sw.WriteLine("\n");
                        sw.WriteLine("О списании оборудования B связи с неисправностью, несоответствием технических характеристик");
                        sw.WriteLine("\n");
                        sw.WriteLine("требованиям устанавливаемого программного обеспечения, отсутствием запасных");
                        sw.WriteLine("\n");
                        sw.WriteLine("частей и окончанием срока полезного использования, а также в соответствии с");
                        sw.WriteLine("\n");
                        sw.WriteLine("приказом от 21.10.2013 N: 215 «О внесении изменений в приказ от 20.10.2009 No 199");
                        sw.WriteLine("\n");
                        sw.WriteLine(" «О порядке списания основных средств и материально - производственных запасов»");
                        sw.WriteLine("\n");
                        sw.WriteLine("подлежит списанию оборудование, указанное в приложении.");
                        sw.WriteLine("\n");
                        for (int i = 0; i < listBox2.Items.Count; i++)
                        {
                            listBox2.SelectedIndex = i;
                            sw.WriteLine(listBox2.SelectedItem.ToString());
                        }
                        sw.Close();
                        myStream.Close();
                    }
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.SelectedItems.Clear();
            for(int i = listBox1.Items.Count - 1; i > 0; i--)
            {
                if(listBox1.Items[i].ToString().ToLower().Contains(textBox7.Text.ToLower()))
                {
                    listBox1.SetSelected(i, true);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox2.SelectedItem);
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        StreamWriter sw = new StreamWriter(myStream);
                        sw.WriteLine("РАСПОРЯЖЕНИЕ");
                        sw.WriteLine("\n");
                        sw.WriteLine("О передаче оборудования");
                        sw.WriteLine("из отдела №");
                        sw.WriteLine(textBox10.Text);
                        sw.WriteLine("в отдел №");
                        sw.WriteLine(textBox9.Text);
                        sw.WriteLine("\n");
                        sw.WriteLine("Оборудования");
                        sw.WriteLine("\n");

                        for (int i = 0; i < listBox2.Items.Count; i++)
                        {
                            listBox2.SelectedIndex = i;
                            sw.WriteLine(listBox2.SelectedItem.ToString());
                        }
                        sw.Close();
                        myStream.Close();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            // listBox1.Items.RemoveAt(listBox2.SelectedIndex);
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        listBox1.Items.Remove(listBox1.SelectedItem);
                        StreamWriter sw = new StreamWriter(myStream);
                        sw.WriteLine("РАСПОРЯЖЕНИЕ");
                        sw.WriteLine("\n");
                        sw.WriteLine("О получении оборудования");
                        sw.WriteLine("отделом №");
                        sw.WriteLine(textBox11.Text);
                        sw.WriteLine("\n");
                        sw.WriteLine("Оборудования");
                        sw.WriteLine("\n");

                        for (int i = 0; i < listBox2.Items.Count; i++)
                        {
                            listBox2.SelectedIndex = i;
                            sw.WriteLine(listBox2.SelectedItem.ToString());
                        }
                        sw.Close();
                        myStream.Close();
                    }
                }
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: «Ari 1.0.0.1» \n Автор программы: Степанов Арсентий Владиславович \n Страна: Россия, Свердловская область, Нижний Тагил");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }
}
