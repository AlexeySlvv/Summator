using System;
using System.Text;
using System.Windows.Forms;
using Summator.Properties;
using SummatorLib;

namespace Summator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private enum EAlg
        {
            Md5 = 0,
            Sha1,
            Sha256,
            Sha512,
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            comboBox.SelectedIndex = (int) EAlg.Md5;
            numericUpDown.Value = Settings.Default.Base64Length;
        }

        /// <summary>
        /// Returns Base64 string if checked
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetBase64IfCheck(string text)
        {
            if (!checkBox.Checked)
                return text;

            if (text == null)
                return null;

            int len = (int) numericUpDown.Value;

            if (len == 0)
                return text.ToBase64();

            return text.ToBase64().Substring(0, len);
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string text in textBoxInput.Lines)
            {
                if (comboBox.SelectedIndex == (int) EAlg.Md5)
                    sb.AppendLine(GetBase64IfCheck(Strings.Md5Sum(text)));
                else if (comboBox.SelectedIndex == (int) EAlg.Sha1)
                    sb.AppendLine(GetBase64IfCheck(Strings.Sha1Sum(text)));
                else if (comboBox.SelectedIndex == (int) EAlg.Sha256)
                    sb.AppendLine(GetBase64IfCheck(Strings.Sha256Sum(text)));
                else if (comboBox.SelectedIndex == (int) EAlg.Sha512)
                    sb.AppendLine(GetBase64IfCheck(Strings.Sha512Sum(text)));
            }
            textBoxOutput.Text = sb.ToString();
        }

        private void mainMenuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    textBoxInput.Lines = ofd.FileNames;

                    StringBuilder sb = new StringBuilder();
                    foreach (string filename in textBoxInput.Lines)
                    {
                        sb.AppendLine($"{filename}:");
                        sb.AppendLine($"\tcrc32:\t{Files.Crc32Sum(filename)}");
                        sb.AppendLine($"\tmd5:\t{Files.Md5Sum(filename)}");
                        sb.AppendLine($"\tsha1:\t{Files.Sha1Sum(filename)}");
                        sb.AppendLine($"\tsha256:\t{Files.Sha256Sum(filename)}");
                        sb.AppendLine($"\tsha512:\t{Files.Sha512Sum(filename)}");
                        sb.AppendLine();
                    }

                    textBoxOutput.Text = sb.ToString();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void mainMenuFileExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Base64Length = (int)numericUpDown.Value;
            Settings.Default.Save();
        }

    }
}
