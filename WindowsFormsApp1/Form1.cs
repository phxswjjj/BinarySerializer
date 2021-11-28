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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var fileSelector = new OpenFileDialog();
            fileSelector.InitialDirectory = Application.StartupPath;
            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                var filePath = fileSelector.FileName;
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("no file path");
                    return;
                }
                using (var fr = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var content = new byte[fr.Length];
                    fr.Read(content, 0, (int)fr.Length);
                    var base64 = Convert.ToBase64String(content);
                    txtSerialized.Text = base64;
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            var base64 = txtSerialized.Text.Trim();
            if (string.IsNullOrEmpty(base64))
                return;

            var fileSelector = new SaveFileDialog();
            fileSelector.InitialDirectory = Application.StartupPath;
            if (fileSelector.ShowDialog() == DialogResult.OK)
            {
                var filePath = fileSelector.FileName;
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("no file path");
                    return;
                }
                var content = Convert.FromBase64String(base64);
                using (var fw = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fw.Write(content, 0, content.Length);
                }
            }
        }
    }
}
