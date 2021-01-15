using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoE_Leveling_Helper.Properties;

namespace PoE_Leveling_Helper
{
    public partial class Form1 : Form
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly DataTable _dataTable;
        private Stream _fileStream;
        private long _lastFileSize = -1;

        public Form1()
        {
            _soundPlayer = new SoundPlayer(Resources.alert);
            _soundPlayer.Load();

            InitializeComponent();

#if DEBUG
            btn_test.Visible = true;
#endif

            txt_poe_folder.Text = Settings.Default.poe_path;
            txt_char_name.Text = Settings.Default.char_name;
            checkBox_alert.Checked = Settings.Default.play_alert_sound;
            checkBox_tts.Checked = Settings.Default.use_tts;

            _dataTable = new DataTable("reminders");
            _dataTable.Columns.Add("Level");
            _dataTable.Columns.Add("Reminder");

            var reader = new StringReader(Settings.Default.data_reminders);
            _dataTable.ReadXml(reader);

            dataGridView1.DataSource = _dataTable;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            OpenLogFile();

            timer1.Start();
        }

        private void btn_browse_poe_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog_poe.ShowDialog() == DialogResult.OK)
            {
                txt_poe_folder.Text = folderBrowserDialog_poe.SelectedPath;
                Settings.Default.poe_path = folderBrowserDialog_poe.SelectedPath;
                Settings.Default.Save();

                _fileStream.Dispose();
            }
        }

        private void LogFileWatchTask()
        {
            try
            {
                if (!_fileStream.CanRead)
                    OpenLogFile();

                using (var reader = new StreamReader(_fileStream))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var m = Regex.Match(line, @"^.*: (?<name>\w+) \(\w+\) is now level (?<level>\d+$)");
                        if (m.Success)
                        {
                            var name = m.Groups["name"].Value;
                            var level = m.Groups["level"].Value;
                            CheckAndNotify(name, level);
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                OpenLogFile();
            }
        }

        private void OpenLogFile()
        {
            try
            {
                var filename = txt_poe_folder.Text + @"\logs\Client.txt";
                FileInfo logFileInfo = new FileInfo(filename);

                _fileStream = logFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                if (_lastFileSize == -1)
                    _lastFileSize = logFileInfo.Length;

                _fileStream.Seek(_lastFileSize, SeekOrigin.Begin);
                _lastFileSize = logFileInfo.Length;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Could not open the Log-File. Please check if the path to your Path of Exile installation is correct.");
            }
        }

        private void CheckAndNotify(string name, string level)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Selected = false;

            string textToSpeak = "";
            bool foundMatch = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    string rLevel = (string) row.Cells[0].Value;
                    string rMessage = (string) row.Cells[1].Value;

                    if (rLevel == level && (txt_char_name.Text == "" || txt_char_name.Text == name))
                    {
                        foundMatch = true;
                        textToSpeak += "Level " + rLevel + ". " + rMessage + ".\r\n";
                        row.Selected = true;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            if (foundMatch && checkBox_alert.Checked)
                _soundPlayer.Play();

            if (checkBox_tts.Checked)
                Speak(textToSpeak);
        }

        // https://www.c-sharpcorner.com/blogs/using-systemspeech-with-net-core-30
        private static void Speak(string textToSpeech, bool wait = false)  
        {  
            // Command to execute PS  
            Execute($@"Add-Type -AssemblyName System.speech;  
            $speak = New-Object System.Speech.Synthesis.SpeechSynthesizer;                           
            $speak.Speak(""{textToSpeech}"");"); // Embedd text  
  
            void Execute(string command)  
            {  
                // create a temp file with .ps1 extension  
                var cFile = System.IO.Path.GetTempPath() + Guid.NewGuid() + ".ps1";  
  
                //Write the .ps1  
                using var tw = new System.IO.StreamWriter(cFile, false, Encoding.UTF8);  
                tw.Write(command);  
  
                // Setup the PS  
                var start =
                    new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = "C:\\windows\\system32\\windowspowershell\\v1.0\\powershell.exe",  // CHUPA MICROSOFT 02-10-2019 23:45
                        LoadUserProfile = false,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"-executionpolicy bypass -File {cFile}",  
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden  
                    };  
  
                //Init the Process  
                var p = System.Diagnostics.Process.Start(start);  
                // The wait may not work! :(  
                if (wait) p.WaitForExit();  
            }  
        }

        private void txt_char_name_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.char_name = txt_char_name.Text;
            Settings.Default.Save();
        }

        private void checkBox_tts_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.use_tts = checkBox_tts.Checked;
            Settings.Default.Save();
        }

        private void checkBox_alert_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.play_alert_sound = checkBox_alert.Checked;
            Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _fileStream.Dispose();

            StringWriter writer = new StringWriter();

            _dataTable.WriteXml(writer, XmlWriteMode.IgnoreSchema, true);

            Settings.Default.data_reminders = writer.ToString();
            Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LogFileWatchTask();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckAndNotify("test", "101");
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will overwrite your current reminders. Continue?", "Attention",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;
            
            openFileDialog1.Filter = @"XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _dataTable.Clear();
                using var stream = openFileDialog1.OpenFile();
                _dataTable.ReadXml(stream);
            }

        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = @"XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.DefaultExt = @".xml";
            saveFileDialog1.AddExtension = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using var stream = saveFileDialog1.OpenFile();
                _dataTable.WriteXml(stream, XmlWriteMode.IgnoreSchema, true);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will clear ALL of your current reminders. Continue?", "Attention",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            _dataTable.Clear();
        }
    }
}
