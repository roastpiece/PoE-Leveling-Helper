
namespace PoE_Leveling_Helper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Reminders = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_uncomplete_all = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comb_part = new System.Windows.Forms.ComboBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.After = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Reminder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Completed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox_mark_complete = new System.Windows.Forms.CheckBox();
            this.checkBox_alert = new System.Windows.Forms.CheckBox();
            this.checkBox_tts = new System.Windows.Forms.CheckBox();
            this.txt_char_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_browse_poe_folder = new System.Windows.Forms.Button();
            this.txt_poe_folder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog_poe = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reminders.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Reminders
            // 
            this.Reminders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Reminders.Controls.Add(this.tabPage1);
            this.Reminders.Controls.Add(this.tabPage2);
            this.Reminders.Location = new System.Drawing.Point(13, 13);
            this.Reminders.Name = "Reminders";
            this.Reminders.SelectedIndex = 0;
            this.Reminders.Size = new System.Drawing.Size(1239, 636);
            this.Reminders.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_uncomplete_all);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comb_part);
            this.tabPage1.Controls.Add(this.btn_clear);
            this.tabPage1.Controls.Add(this.btn_export);
            this.tabPage1.Controls.Add(this.btn_import);
            this.tabPage1.Controls.Add(this.btn_test);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1231, 608);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reminders";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_uncomplete_all
            // 
            this.btn_uncomplete_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_uncomplete_all.Location = new System.Drawing.Point(1034, 582);
            this.btn_uncomplete_all.Name = "btn_uncomplete_all";
            this.btn_uncomplete_all.Size = new System.Drawing.Size(113, 23);
            this.btn_uncomplete_all.TabIndex = 7;
            this.btn_uncomplete_all.Text = "Un-Complete All";
            this.btn_uncomplete_all.UseVisualStyleBackColor = true;
            this.btn_uncomplete_all.Click += new System.EventHandler(this.btn_uncomplete_all_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Help;
            this.label3.Location = new System.Drawing.Point(273, 586);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select Part";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // comb_part
            // 
            this.comb_part.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comb_part.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_part.FormattingEnabled = true;
            this.comb_part.Items.AddRange(new object[] {
            "",
            "Part 1",
            "Part 2"});
            this.comb_part.Location = new System.Drawing.Point(341, 582);
            this.comb_part.Name = "comb_part";
            this.comb_part.Size = new System.Drawing.Size(80, 23);
            this.comb_part.TabIndex = 5;
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Location = new System.Drawing.Point(1153, 582);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 4;
            this.btn_clear.Text = "Delete All";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_export
            // 
            this.btn_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_export.Location = new System.Drawing.Point(84, 582);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 3;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_import
            // 
            this.btn_import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_import.Location = new System.Drawing.Point(3, 582);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(75, 23);
            this.btn_import.TabIndex = 2;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_test
            // 
            this.btn_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_test.Location = new System.Drawing.Point(165, 582);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(102, 23);
            this.btn_test.TabIndex = 1;
            this.btn_test.Text = "Test selected";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Level,
            this.After,
            this.Reminder,
            this.Completed});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1225, 573);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridView1_CellContextMenuStripNeeded);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_DefaultValuesNeeded);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // Level
            // 
            this.Level.DataPropertyName = "Level";
            this.Level.HeaderText = "Type Value";
            this.Level.Name = "Level";
            this.Level.Width = 160;
            // 
            // After
            // 
            this.After.DataPropertyName = "After";
            this.After.HeaderText = "After";
            this.After.Name = "After";
            // 
            // Reminder
            // 
            this.Reminder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Reminder.DataPropertyName = "Reminder";
            this.Reminder.HeaderText = "Reminder";
            this.Reminder.Name = "Reminder";
            // 
            // Completed
            // 
            this.Completed.DataPropertyName = "Completed";
            this.Completed.FalseValue = "false";
            this.Completed.HeaderText = "Completed";
            this.Completed.IndeterminateValue = "false";
            this.Completed.Name = "Completed";
            this.Completed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Completed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Completed.TrueValue = "true";
            this.Completed.Width = 70;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox_mark_complete);
            this.tabPage2.Controls.Add(this.checkBox_alert);
            this.tabPage2.Controls.Add(this.checkBox_tts);
            this.tabPage2.Controls.Add(this.txt_char_name);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btn_browse_poe_folder);
            this.tabPage2.Controls.Add(this.txt_poe_folder);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1231, 608);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_mark_complete
            // 
            this.checkBox_mark_complete.AutoSize = true;
            this.checkBox_mark_complete.Cursor = System.Windows.Forms.Cursors.Help;
            this.checkBox_mark_complete.Location = new System.Drawing.Point(3, 152);
            this.checkBox_mark_complete.Name = "checkBox_mark_complete";
            this.checkBox_mark_complete.Size = new System.Drawing.Size(180, 19);
            this.checkBox_mark_complete.TabIndex = 8;
            this.checkBox_mark_complete.Text = "Mark fired alerts as Complete";
            this.toolTip1.SetToolTip(this.checkBox_mark_complete, "Complete alerts won\'t trigger an alert.");
            this.checkBox_mark_complete.UseVisualStyleBackColor = true;
            this.checkBox_mark_complete.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_alert
            // 
            this.checkBox_alert.AutoSize = true;
            this.checkBox_alert.Checked = true;
            this.checkBox_alert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_alert.Location = new System.Drawing.Point(3, 127);
            this.checkBox_alert.Name = "checkBox_alert";
            this.checkBox_alert.Size = new System.Drawing.Size(113, 19);
            this.checkBox_alert.TabIndex = 7;
            this.checkBox_alert.Text = "Play Alert Sound";
            this.checkBox_alert.UseVisualStyleBackColor = true;
            this.checkBox_alert.CheckedChanged += new System.EventHandler(this.checkBox_alert_CheckedChanged);
            // 
            // checkBox_tts
            // 
            this.checkBox_tts.AutoSize = true;
            this.checkBox_tts.Checked = true;
            this.checkBox_tts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_tts.Location = new System.Drawing.Point(4, 102);
            this.checkBox_tts.Name = "checkBox_tts";
            this.checkBox_tts.Size = new System.Drawing.Size(66, 19);
            this.checkBox_tts.TabIndex = 6;
            this.checkBox_tts.Text = "Use TTS";
            this.checkBox_tts.UseVisualStyleBackColor = true;
            this.checkBox_tts.CheckedChanged += new System.EventHandler(this.checkBox_tts_CheckedChanged);
            // 
            // txt_char_name
            // 
            this.txt_char_name.Location = new System.Drawing.Point(3, 73);
            this.txt_char_name.Name = "txt_char_name";
            this.txt_char_name.Size = new System.Drawing.Size(237, 23);
            this.txt_char_name.TabIndex = 4;
            this.txt_char_name.TextChanged += new System.EventHandler(this.txt_char_name_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Character Name";
            // 
            // btn_browse_poe_folder
            // 
            this.btn_browse_poe_folder.Location = new System.Drawing.Point(247, 29);
            this.btn_browse_poe_folder.Name = "btn_browse_poe_folder";
            this.btn_browse_poe_folder.Size = new System.Drawing.Size(75, 23);
            this.btn_browse_poe_folder.TabIndex = 2;
            this.btn_browse_poe_folder.Text = "Browse";
            this.btn_browse_poe_folder.UseVisualStyleBackColor = true;
            this.btn_browse_poe_folder.Click += new System.EventHandler(this.btn_browse_poe_folder_Click);
            // 
            // txt_poe_folder
            // 
            this.txt_poe_folder.Location = new System.Drawing.Point(4, 29);
            this.txt_poe_folder.Name = "txt_poe_folder";
            this.txt_poe_folder.Size = new System.Drawing.Size(237, 23);
            this.txt_poe_folder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path of Exile Directory";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.Reminders);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "Form1";
            this.Text = "PoE Leveling Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Reminders.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Reminders;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_browse_poe_folder;
        private System.Windows.Forms.TextBox txt_poe_folder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_poe;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_char_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_tts;
        private System.Windows.Forms.CheckBox checkBox_alert;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comb_part;
        private System.Windows.Forms.Button btn_uncomplete_all;
        private System.Windows.Forms.CheckBox checkBox_mark_complete;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewComboBoxColumn After;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reminder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Completed;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

