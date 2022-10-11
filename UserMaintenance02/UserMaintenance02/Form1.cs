using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance02.Entities;

namespace UserMaintenance02
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.LastName;
            label2.Text = Resource1.FirstName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;
            button3.Text = Resource1.Delete;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User u = new User()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Vesszővel tagolt fájl (*.csv) | *.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
                foreach (User u in users)
                {
                    sw.WriteLine($"{u.ID};{u.FullName}");
                }
                sw.Close();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var userID = ((User)listBox1.SelectedItem).ID;

            var torlendo = (from u in users
                           where u.ID == userID
                           select u).FirstOrDefault();
            users.Remove(torlendo);
            
        }
    }
}
