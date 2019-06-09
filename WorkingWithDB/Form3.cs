using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Forms;

namespace WorkingWithDB
{
    public partial class Form3 : MetroForm
    {
    
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="admin" && textBox2.Text=="admin")
            {
                Form f1 = new Form1();
                f1.Show();

                this.Hide();
            }
            else
            {
                 textBox1.Text = "";
                 textBox2.Text = "";
                 MessageBox.Show("Неправильный логин или пароль!");
            }
                
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}