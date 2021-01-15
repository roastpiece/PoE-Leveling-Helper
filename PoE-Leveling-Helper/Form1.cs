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
using PoE_Leveling_Helper.Helper;
using PoE_Leveling_Helper.Model;
using PoE_Leveling_Helper.Properties;

namespace PoE_Leveling_Helper
{
    public partial class Form1 : Form
    {
        private readonly SoundPlayer _soundPlayer;
        private readonly DataTable _dataTable;
        private Stream _fileStream;
        private long _lastFileSize = -1;
        private readonly AutoCompleteStringCollection _areasPart1AutoCompleteSource;
        private readonly AutoCompleteStringCollection _areasPart2AutoCompleteSource;

        private struct ColumnIndex
        {
            public static int Type = 0;
            public static int TypeValue = 1;
            public static int Reminder = 2;
            public static int Completed = 3;
        }

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
            checkBox_mark_complete.Checked = Settings.Default.mark_complete;

            _dataTable = new DataTable("reminders");
            _dataTable.Columns.Add("Type");
            _dataTable.Columns.Add("Level");
            _dataTable.Columns.Add("Reminder");
            _dataTable.Columns.Add("Completed");

            var reader = new StringReader(Settings.Default.data_reminders);
            _dataTable.ReadXml(reader);

            dataGridView1.DataSource = _dataTable;

            _areasPart1AutoCompleteSource = new AutoCompleteStringCollection();
            _areasPart1AutoCompleteSource.AddRange(Resources.areas_part1.Split("\r\n"));
            _areasPart2AutoCompleteSource = new AutoCompleteStringCollection();
            _areasPart2AutoCompleteSource.AddRange(Resources.areas_part2.Split("\r\n"));

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
                        var mLevel = Regex.Match(line, @"^.*: (?<name>\w+) \(\w+\) is now level (?<level>\d+$)");
                        var mZone = Regex.Match(line, @"^.*: You have entered (?<zone>.*)\.$");

                        if (mLevel.Success || mZone.Success)
                            ClearSelection();

                        if (mLevel.Success)
                        {
                            var name = mLevel.Groups["name"].Value;
                            var level = mLevel.Groups["level"].Value;
                            CheckAndNotifyLevel(name, level);
                        }

                        if (mZone.Success)
                        {
                            var zone = mZone.Groups["zone"].Value;
                            CheckAndNotifyZone(zone);
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

        private void ClearSelection()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Selected = false;
        }

        private void CheckAndNotifyLevel(string name, string level)
        {
            string textToSpeak = "";
            bool foundMatch = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    string rLevel = (string) row.Cells[ColumnIndex.TypeValue].Value;
                    string rMessage = (string) row.Cells[ColumnIndex.Reminder].Value;

                    object rCompletedVal = row.Cells[ColumnIndex.Completed].Value;
                    if (rCompletedVal is string completedVal && completedVal == "true")
                        break;

                    if (rLevel == level && (txt_char_name.Text == "" || txt_char_name.Text == name))
                    {
                        foundMatch = true;
                        textToSpeak += "Level " + rLevel + ". " + rMessage + ".\r\n";
                        row.Selected = true;

                        if (checkBox_mark_complete.Checked)
                            row.Cells[ColumnIndex.Completed].Value = true;
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
                TextToSpeech.Speak(textToSpeak);
        }

        private void CheckAndNotifyZone(string zone)
        {
            string textToSpeak = "";
            bool foundMatch = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    if (row.IsNewRow)
                        break;

                    string rType = (string) row.Cells[ColumnIndex.Type].Value;
                    string rZone = (string) row.Cells[ColumnIndex.TypeValue].Value;
                    string rMessage = (string) row.Cells[ColumnIndex.Reminder].Value;

                    object rCompletedVal = row.Cells[ColumnIndex.Completed].Value;
                    if (rCompletedVal is string completedVal && completedVal == "true")
                        break;

                    if (rZone == zone && (comb_part.Text == "" || rType == @"Zone: " + comb_part.Text))
                    {
                        foundMatch = true;
                        textToSpeak += "Zone " + rZone + ". " + rMessage + ".\r\n";
                        row.Selected = true;

                        if (checkBox_mark_complete.Checked)
                            row.Cells[ColumnIndex.Completed].Value = "true";
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
                TextToSpeech.Speak(textToSpeak);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.mark_complete = checkBox_mark_complete.Checked;
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
            CheckAndNotifyLevel("test", "101");
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == ColumnIndex.TypeValue)
            {
                try
                {
                    string type = (string) dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[ColumnIndex.Type].Value;
                    if (type != ReminderType.ZonePart1 && type != ReminderType.ZonePart2)
                        return;

                    var autoCompleteSource = type == ReminderType.ZonePart1
                        ? _areasPart1AutoCompleteSource
                        : _areasPart2AutoCompleteSource;

                    if (e.Control is TextBox boxControl)
                    {
                        boxControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        boxControl.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        boxControl.AutoCompleteCustomSource = autoCompleteSource;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void btn_uncomplete_all_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Cells[ColumnIndex.Completed].Value = "false";
        }
    }
}
