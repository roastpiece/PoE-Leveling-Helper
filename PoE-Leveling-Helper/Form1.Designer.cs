
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
            this.Reminders = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.Reminders.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.Reminders.Size = new System.Drawing.Size(869, 600);
            this.Reminders.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_clear);
            this.tabPage1.Controls.Add(this.btn_export);
            this.tabPage1.Controls.Add(this.btn_import);
            this.tabPage1.Controls.Add(this.btn_test);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(861, 572);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reminders";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(783, 546);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 4;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(84, 546);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 3;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(3, 546);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(75, 23);
            this.btn_import.TabIndex = 2;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_test
            // 
            this.btn_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_test.Location = new System.Drawing.Point(165, 546);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 1;
            this.btn_test.Text = "test button";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Visible = false;
            this.btn_test.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(855, 537);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
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
            this.tabPage2.Size = new System.Drawing.Size(861, 572);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 625);
            this.Controls.Add(this.Reminders);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Reminders.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
    }
}

