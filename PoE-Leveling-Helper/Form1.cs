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
using System.Threading.Tasks;
using System.Windows.Forms;
using PoE_Leveling_Helper.Properties;

namespace PoE_Leveling_Helper
{
    public partial class Form1 : Form
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly DataTable _dataTable;
        private long _lastFileLength;
        public Form1()
        {
            _soundPlayer = new SoundPlayer(Resources.alert);
            _soundPlayer.Load();

            InitializeComponent();

            txt_poe_folder.Text = Settings.Default.poe_path;
            txt_char_name.Text = Settings.Default.char_name;
            checkBox_alert.Checked = Settings.Default.play_alert_sound;
            checkBox_tts.Checked = Settings.Default.use_tts;

            _dataTable = new DataTable();
            _dataTable.TableName = "reminders";
            _dataTable.Columns.Add("Level");
            _dataTable.Columns.Add("Reminder");

            var reader = new StringReader(Settings.Default.data_reminders);
            _dataTable.ReadXml(reader);

            dataGridView1.DataSource = _dataTable;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btn_browse_poe_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog_poe.ShowDialog() == DialogResult.OK)
            {
                txt_poe_folder.Text = folderBrowserDialog_poe.SelectedPath;
                Settings.Default.poe_path = folderBrowserDialog_poe.SelectedPath;
                Settings.Default.Save();

                fileSystemWatcher1.Path = folderBrowserDialog_poe.SelectedPath + @"\logs\";
                fileSystemWatcher1.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                fileSystemWatcher1.Filter = "Client.txt";

                var filename = txt_poe_folder.Text + @"\logs\Client.txt";
                FileInfo logFileInfo = new FileInfo(filename);
                _lastFileLength = logFileInfo.Length;
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            var filename = txt_poe_folder.Text + @"\logs\Client.txt";
            FileInfo logFileInfo = new FileInfo(filename);

            // ReSharper disable once ConvertToUsingDeclaration
            using (var stream = logFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Seek(_lastFileLength, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                {
                    var linesString = reader.ReadToEnd();
                    var lines = linesString.Split("\r\n");

                    foreach (var line in lines)
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
        }

        private void CheckAndNotify(string name, string level)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string rLevel = (string)row.Cells[0].Value;
                string rMessage = (string) row.Cells[1].Value;

                if (rLevel == level && (txt_char_name.Text == "" || txt_char_name.Text == name))
                {
                    if (checkBox_alert.Checked)
                        _soundPlayer.Play();

                    if (checkBox_tts.Checked)
                        Speak("Level " + rLevel + ". " + rMessage);

                    row.Selected = true;
                }
            }
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
            StringWriter writer = new StringWriter();
            //_dataTable.Rows.Add("50", "Yay");

            _dataTable.WriteXml(writer, XmlWriteMode.IgnoreSchema, true);

            Settings.Default.data_reminders = writer.ToString();
            Settings.Default.Save();
        }
    }
}
