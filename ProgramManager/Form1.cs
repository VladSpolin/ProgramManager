using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ProgramManager
{
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        public Form1()
        {
            InitializeComponent();
            dataBase.openConnection();
            checkedListBox1.Items.Clear();
            SqlCommand sqlCommand = new SqlCommand($"select programName from Programs", dataBase.GetConnection());
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                checkedListBox1.Items.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadProgram downloadProgram = new DownloadProgram();
            foreach (var checkeditem in checkedListBox1.CheckedItems)
            {
                downloadProgram.Download(checkeditem.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
            List<DirectoryInfo> dirlist = new List<DirectoryInfo>(directoryInfo.GetDirectories());
            foreach (DirectoryInfo dir in dirlist)
            {
                listBox1.Items.Add(dir);
            }
            List<FileInfo> filelist = new List<FileInfo>(directoryInfo.GetFiles());
            foreach (FileInfo file in filelist)
            {
                listBox1.Items.Add(file);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Path.GetExtension(Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString())) == "")
            {
                textBox1.Text = Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString());
                listBox1.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
                List<DirectoryInfo> dirlist = new List<DirectoryInfo>(directoryInfo.GetDirectories());
                foreach (DirectoryInfo dir in dirlist)
                {
                    listBox1.Items.Add(dir);
                }
                List<FileInfo> filelist = new List<FileInfo>(directoryInfo.GetFiles());
                foreach (FileInfo file in filelist)
                {
                    listBox1.Items.Add(file);
                }
            }
            else
            {
                Process.Start(Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString()));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains("\\"))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.LastIndexOf('\\'));
                listBox1.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
                List<DirectoryInfo> dirlist = new List<DirectoryInfo>(directoryInfo.GetDirectories());
                foreach (DirectoryInfo dir in dirlist)
                {
                    listBox1.Items.Add(dir);
                }
                List<FileInfo> filelist = new List<FileInfo>(directoryInfo.GetFiles());
                foreach (FileInfo file in filelist)
                {
                    listBox1.Items.Add(file);
                }
            }          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Delete(Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString()));
            listBox1.Items.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
            List<DirectoryInfo> dirlist = new List<DirectoryInfo>(directoryInfo.GetDirectories());
            foreach (DirectoryInfo dir in dirlist)
            {
                listBox1.Items.Add(dir);
            }
            List<FileInfo> filelist = new List<FileInfo>(directoryInfo.GetFiles());
            foreach (FileInfo file in filelist)
            {
                listBox1.Items.Add(file);
            }
        }
    }
}
