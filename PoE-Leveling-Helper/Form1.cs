using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Text.Json;
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
        private readonly AutoCompleteStringCollection _gemsAutoCompleteSource;
        private Dictionary<string, Gem> _gems;

        private struct ColumnIndex
        {
            public const int Type = 0;
            public const int TypeValue = 1;
            public const int After = 2;
            public const int Reminder = 3;
            public const int Completed = 4;
        }

        private struct Types
        {
            public const string Level = "Level";
            public const string Zone = "Zone";
            public const string Gem = "Gem";
            public const string ZoneP1 = "Zone: Part 1";
            public const string ZoneP2 = "Zone: Part 2";
        }

        public Form1()
        {
            _soundPlayer = new SoundPlayer(Resources.alert);
            _soundPlayer.Load();

            InitializeComponent();

            txt_poe_folder.Text = Settings.Default.poe_path;
            txt_char_name.Text = Settings.Default.char_name;
            checkBox_alert.Checked = Settings.Default.play_alert_sound;
            checkBox_tts.Checked = Settings.Default.use_tts;
            checkBox_mark_complete.Checked = Settings.Default.mark_complete;

            _dataTable = new DataTable("reminders");
            _dataTable.Columns.Add("Type");
            _dataTable.Columns.Add("Level");
            _dataTable.Columns.Add("After");
            _dataTable.Columns.Add("Reminder");
            _dataTable.Columns.Add("Completed");

            var reader = new StringReader(Settings.Default.data_reminders);
            _dataTable.ReadXml(reader);

            dataGridView1.DataSource = _dataTable;
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["Type"]).Items.AddRange(new[] { Types.Level, Types.Gem, Types.ZoneP1, Types.ZoneP2 });

            _areasPart1AutoCompleteSource = new AutoCompleteStringCollection();
            _areasPart1AutoCompleteSource.AddRange(Resources.areas_part1.Split("\n"));
            _areasPart2AutoCompleteSource = new AutoCompleteStringCollection();
            _areasPart2AutoCompleteSource.AddRange(Resources.areas_part2.Split("\n"));

            var skillGems = JsonSerializer.Deserialize<Gem[]>(Resources.skillGems);
            var supportGems = JsonSerializer.Deserialize<Gem[]>(Resources.supportGems);
            var gems = skillGems.Concat(supportGems);
            _gemsAutoCompleteSource = new AutoCompleteStringCollection();
            _gemsAutoCompleteSource.AddRange(gems.Select(v => v.name).ToArray());

            _gems = new Dictionary<string, Gem>();
            foreach (var gem in gems)
            {
                _gems.Add(gem.name, gem);
            }

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

                _fileStream?.Dispose();
            }
        }

        private void LogFileWatchTask()
        {
            if (_fileStream == null)
                return;

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
                            dataGridView1.ClearSelection();

                        if (mLevel.Success)
                        {
                            var name = mLevel.Groups["name"].Value;
                            var level = mLevel.Groups["level"].Value;
                            CheckAndNotify(Types.Level, name, level);
                        }

                        if (mZone.Success)
                        {
                            var zone = mZone.Groups["zone"].Value;
                            CheckAndNotify(Types.Zone, zone);
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
                Reminders.SelectTab(tabPage2);
            }
        }

        private void CheckAndNotify(string type, string value, string name = null)
        {
            string textToSpeak = "";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                if (row.IsNewRow)
                    continue;

                object rAfter = row.Cells[ColumnIndex.After].Value;
                if (rAfter is string strAfter && strAfter != "")
                {
                    var iAfter = Int32.Parse(strAfter) - 1;
                    object rOther = dataGridView1.Rows[iAfter].Cells[ColumnIndex.Completed].Value;
                    if (!isChecked(rOther))
                        continue;
                }

                object rCompletedVal = row.Cells[ColumnIndex.Completed].Value;
                if (isChecked(rCompletedVal))
                    continue;

                switch (type)
                {
                    case Types.Level:
                        textToSpeak += GetMessageLevel(name, int.Parse(value), row);
                        break;

                    case Types.Zone:
                    case Types.ZoneP1:
                    case Types.ZoneP2:
                        textToSpeak += GetMessageZone(value, row);
                        break;

                    case Types.Gem:
                        textToSpeak += GetMessageGem(int.Parse(value), row);
                        break;

                    default:
                        break;
                }
            }

            if (textToSpeak != "" && checkBox_alert.Checked)
                _soundPlayer.Play();

            if (textToSpeak != "" && checkBox_tts.Checked)
                TextToSpeech.Speak(textToSpeak);
        }

        private string GetMessageLevel(string name, int level, DataGridViewRow row)
        {
            string rType = row.Cells[ColumnIndex.Type].Value.ToString();
            if (rType != "Level")
                return "";

            int rLevel = int.Parse(row.Cells[ColumnIndex.TypeValue].Value.ToString());
            string rMessage = row.Cells[ColumnIndex.Reminder].Value.ToString();

            if (rLevel <= level && (txt_char_name.Text == "" || txt_char_name.Text == name))
            {
                row.Selected = true;

                if (checkBox_mark_complete.Checked)
                    row.Cells[ColumnIndex.Completed].Value = true;

                return "Level " + rLevel + ". " + rMessage + ".\r\n";
            }

            return "";
        }

        private string GetMessageZone(string zone, DataGridViewRow row)
        {
            string rType = row.Cells[ColumnIndex.Type].Value.ToString();
            string rZone = row.Cells[ColumnIndex.TypeValue].Value.ToString();
            string rMessage = row.Cells[ColumnIndex.Reminder].Value.ToString();

            if (rZone == zone && (comb_part.Text == "" || rType == @"Zone: " + comb_part.Text))
            {
                row.Selected = true;

                if (checkBox_mark_complete.Checked)
                    row.Cells[ColumnIndex.Completed].Value = true;

                return "Zone " + rZone + ". " + rMessage + ".\r\n";
            }

            return "";
        }

        private string GetMessageGem(int level, DataGridViewRow row)
        {
            string rMessage = row.Cells[ColumnIndex.Reminder].Value.ToString();
            Gem gem;
            if (_gems.TryGetValue(row.Cells[ColumnIndex.TypeValue].Value.ToString(), out gem))
            {
                if (level < gem.reqLevel)
                    return "";

                if (checkBox_mark_complete.Checked)
                    row.Cells[ColumnIndex.Completed].Value = true;

                return "Gem " + gem.name + ": " + rMessage;
            }
            return "";
        }

        private bool isChecked(object val)
        {
            return (val != null && val is not DBNull && Boolean.Parse((string)val));
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
            _fileStream?.Dispose();

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
            var row = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
            var type = (string)row.Cells[ColumnIndex.Type].Value;
            var val = (string)row.Cells[ColumnIndex.TypeValue].Value;

            Gem gem;
            if (type == Types.Gem && _gems.TryGetValue(val, out gem))
                val = gem.reqLevel.ToString();

            CheckAndNotify(type, val);
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
                    string type = (string)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[ColumnIndex.Type].Value;

                    AutoCompleteStringCollection autoCompleteSource;
                    switch (type)
                    {
                        case Types.ZoneP1:
                            autoCompleteSource = _areasPart1AutoCompleteSource;
                            break;
                        case Types.ZoneP2:
                            autoCompleteSource = _areasPart2AutoCompleteSource;
                            break;
                        case Types.Gem:
                            autoCompleteSource = _gemsAutoCompleteSource;
                            break;
                        default:
                            return;
                    }

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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var after = ((DataGridViewComboBoxColumn)dataGridView1.Columns["After"]);
            after.Items.Clear();
            after.Items.Add("");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                after.Items.Add((row.Index + 1).ToString());
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.SetValues(new object[] { Types.Level, "", "", "", false });
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.SelectedCells[0].OwningRow;
            dataGridView1.Rows.Remove(row);
        }

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Rows[e.RowIndex].Selected = true;
            e.ContextMenuStrip = contextMenuStrip1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1bm_5CbVppiapbR7BWYjtByKEK8i0xZS9ARDx9tDgUeA/edit?usp=sharing");
        }

        private void dataGridView1_AutoComplete(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnIndex.TypeValue)
            {
                try
                {
                    string type = (string)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[ColumnIndex.Type].Value;
                    switch (type)
                    {
                        case Types.Gem:
                            var row = dataGridView1.Rows[e.RowIndex];
                            Gem gem;
                            if (row.Cells[ColumnIndex.Reminder].Value.ToString() == "" && _gems.TryGetValue(dataGridView1.Rows[e.RowIndex].Cells[ColumnIndex.TypeValue].Value.ToString(), out gem))
                            {
                                var reminder = String.Format("Level {0}.", gem.reqLevel);
                                if (gem.quest != null)
                                    reminder += String.Format(" Quest: {0}; {1}.", gem.quest.objective, gem.quest.completion);
                                if (gem.vendor != null)
                                    reminder += String.Format(" Vendor: {0}, after Quest: {1}.", gem.vendor.name, gem.vendor.quest);

                                row.Cells[ColumnIndex.Reminder].Value = reminder;
                            }
                            break;
                        default:
                            return;
                    }
                }

                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }
}
