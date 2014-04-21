namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxMACFolder = new System.Windows.Forms.TextBox();
            this.buttonMAC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxRenumber = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxReverseRoutes = new System.Windows.Forms.CheckBox();
            this.checkBoxNewVoice = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxVoiceExtension = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxVoiceFolder = new System.Windows.Forms.TextBox();
            this.buttonProcessGPX = new System.Windows.Forms.Button();
            this.buttonExit2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOutputGPX = new System.Windows.Forms.TextBox();
            this.textBoxInputGPX = new System.Windows.Forms.TextBox();
            this.buttonInputGPXSelect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonClear2 = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAtlas = new System.Windows.Forms.ComboBox();
            this.buttonDestination = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDestination = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPNG = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMACFolder
            // 
            this.textBoxMACFolder.Location = new System.Drawing.Point(16, 69);
            this.textBoxMACFolder.Name = "textBoxMACFolder";
            this.textBoxMACFolder.Size = new System.Drawing.Size(365, 20);
            this.textBoxMACFolder.TabIndex = 3;
            this.textBoxMACFolder.TextChanged += new System.EventHandler(this.textBoxMACFolder_TextChanged);
            // 
            // buttonMAC
            // 
            this.buttonMAC.Location = new System.Drawing.Point(400, 63);
            this.buttonMAC.Name = "buttonMAC";
            this.buttonMAC.Size = new System.Drawing.Size(26, 26);
            this.buttonMAC.TabIndex = 6;
            this.buttonMAC.Text = "...";
            this.buttonMAC.UseVisualStyleBackColor = true;
            this.buttonMAC.Click += new System.EventHandler(this.selectfilenamebutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mobile Atlas Creator Folder (e.g.C:\\Program Files\\Mobile Atlas Creator\\atlases):";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(12, 340);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(455, 229);
            this.listView1.TabIndex = 22;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            this.columnHeader1.Width = 398;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(459, 297);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxRenumber);
            this.tabPage2.Controls.Add(this.buttonClear);
            this.tabPage2.Controls.Add(this.checkBoxReverseRoutes);
            this.tabPage2.Controls.Add(this.checkBoxNewVoice);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.textBoxVoiceExtension);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBoxVoiceFolder);
            this.tabPage2.Controls.Add(this.buttonProcessGPX);
            this.tabPage2.Controls.Add(this.buttonExit2);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.textBoxOutputGPX);
            this.tabPage2.Controls.Add(this.textBoxInputGPX);
            this.tabPage2.Controls.Add(this.buttonInputGPXSelect);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(451, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VoiceNavigation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxRenumber
            // 
            this.checkBoxRenumber.AutoSize = true;
            this.checkBoxRenumber.Location = new System.Drawing.Point(22, 239);
            this.checkBoxRenumber.Name = "checkBoxRenumber";
            this.checkBoxRenumber.Size = new System.Drawing.Size(128, 17);
            this.checkBoxRenumber.TabIndex = 70;
            this.checkBoxRenumber.Text = "Renumber Waypoints";
            this.checkBoxRenumber.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(254, 232);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(86, 26);
            this.buttonClear.TabIndex = 69;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBoxReverseRoutes
            // 
            this.checkBoxReverseRoutes.AutoSize = true;
            this.checkBoxReverseRoutes.Location = new System.Drawing.Point(22, 216);
            this.checkBoxReverseRoutes.Name = "checkBoxReverseRoutes";
            this.checkBoxReverseRoutes.Size = new System.Drawing.Size(103, 17);
            this.checkBoxReverseRoutes.TabIndex = 68;
            this.checkBoxReverseRoutes.Text = "Reverse Routes";
            this.checkBoxReverseRoutes.UseVisualStyleBackColor = true;
            this.checkBoxReverseRoutes.CheckedChanged += new System.EventHandler(this.checkBoxReverseRoutes_CheckedChanged);
            // 
            // checkBoxNewVoice
            // 
            this.checkBoxNewVoice.AutoSize = true;
            this.checkBoxNewVoice.Location = new System.Drawing.Point(22, 193);
            this.checkBoxNewVoice.Name = "checkBoxNewVoice";
            this.checkBoxNewVoice.Size = new System.Drawing.Size(158, 17);
            this.checkBoxNewVoice.TabIndex = 67;
            this.checkBoxNewVoice.Text = "Create New Voice File Tags";
            this.checkBoxNewVoice.UseVisualStyleBackColor = true;
            this.checkBoxNewVoice.CheckedChanged += new System.EventHandler(this.checkBoxNewVoice_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Voice File Extension:";
            // 
            // textBoxVoiceExtension
            // 
            this.textBoxVoiceExtension.Location = new System.Drawing.Point(240, 155);
            this.textBoxVoiceExtension.Name = "textBoxVoiceExtension";
            this.textBoxVoiceExtension.Size = new System.Drawing.Size(147, 20);
            this.textBoxVoiceExtension.TabIndex = 64;
            this.textBoxVoiceExtension.Text = ".mp3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Smartphone Voice Folder:";
            // 
            // textBoxVoiceFolder
            // 
            this.textBoxVoiceFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxVoiceFolder.Location = new System.Drawing.Point(22, 155);
            this.textBoxVoiceFolder.Name = "textBoxVoiceFolder";
            this.textBoxVoiceFolder.Size = new System.Drawing.Size(166, 20);
            this.textBoxVoiceFolder.TabIndex = 62;
            this.textBoxVoiceFolder.Text = "E:\\VR_Voices";
            // 
            // buttonProcessGPX
            // 
            this.buttonProcessGPX.Location = new System.Drawing.Point(162, 232);
            this.buttonProcessGPX.Name = "buttonProcessGPX";
            this.buttonProcessGPX.Size = new System.Drawing.Size(86, 26);
            this.buttonProcessGPX.TabIndex = 61;
            this.buttonProcessGPX.Text = "Process";
            this.buttonProcessGPX.UseVisualStyleBackColor = true;
            this.buttonProcessGPX.Click += new System.EventHandler(this.buttonProcessGPX_Click);
            // 
            // buttonExit2
            // 
            this.buttonExit2.Location = new System.Drawing.Point(346, 232);
            this.buttonExit2.Name = "buttonExit2";
            this.buttonExit2.Size = new System.Drawing.Size(86, 26);
            this.buttonExit2.TabIndex = 60;
            this.buttonExit2.Text = "Exit";
            this.buttonExit2.UseVisualStyleBackColor = true;
            this.buttonExit2.Click += new System.EventHandler(this.buttonExit2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Output GPX Route File:";
            // 
            // textBoxOutputGPX
            // 
            this.textBoxOutputGPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputGPX.Location = new System.Drawing.Point(22, 102);
            this.textBoxOutputGPX.Name = "textBoxOutputGPX";
            this.textBoxOutputGPX.Size = new System.Drawing.Size(365, 20);
            this.textBoxOutputGPX.TabIndex = 57;
            // 
            // textBoxInputGPX
            // 
            this.textBoxInputGPX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInputGPX.Location = new System.Drawing.Point(22, 52);
            this.textBoxInputGPX.Name = "textBoxInputGPX";
            this.textBoxInputGPX.Size = new System.Drawing.Size(365, 20);
            this.textBoxInputGPX.TabIndex = 54;
            this.textBoxInputGPX.TextChanged += new System.EventHandler(this.textBoxInputGPX_TextChanged);
            // 
            // buttonInputGPXSelect
            // 
            this.buttonInputGPXSelect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonInputGPXSelect.Location = new System.Drawing.Point(406, 46);
            this.buttonInputGPXSelect.Name = "buttonInputGPXSelect";
            this.buttonInputGPXSelect.Size = new System.Drawing.Size(26, 26);
            this.buttonInputGPXSelect.TabIndex = 55;
            this.buttonInputGPXSelect.Text = "...";
            this.buttonInputGPXSelect.UseVisualStyleBackColor = true;
            this.buttonInputGPXSelect.Click += new System.EventHandler(this.buttonInputGPXSelect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Input GPX Route File:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonClear2);
            this.tabPage1.Controls.Add(this.buttonProcess);
            this.tabPage1.Controls.Add(this.buttonExit);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxAtlas);
            this.tabPage1.Controls.Add(this.buttonDestination);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxDestination);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxPNG);
            this.tabPage1.Controls.Add(this.textBoxMACFolder);
            this.tabPage1.Controls.Add(this.buttonMAC);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(451, 271);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mobile Atlas Creator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonClear2
            // 
            this.buttonClear2.Location = new System.Drawing.Point(181, 226);
            this.buttonClear2.Name = "buttonClear2";
            this.buttonClear2.Size = new System.Drawing.Size(86, 26);
            this.buttonClear2.TabIndex = 70;
            this.buttonClear2.Text = "Clear";
            this.buttonClear2.UseVisualStyleBackColor = true;
            this.buttonClear2.Click += new System.EventHandler(this.buttonClear2_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(88, 226);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(86, 26);
            this.buttonProcess.TabIndex = 53;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click_1);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(273, 226);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(86, 26);
            this.buttonExit.TabIndex = 52;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Atlas:";
            // 
            // comboBoxAtlas
            // 
            this.comboBoxAtlas.FormattingEnabled = true;
            this.comboBoxAtlas.Location = new System.Drawing.Point(155, 172);
            this.comboBoxAtlas.Name = "comboBoxAtlas";
            this.comboBoxAtlas.Size = new System.Drawing.Size(226, 21);
            this.comboBoxAtlas.TabIndex = 50;
            // 
            // buttonDestination
            // 
            this.buttonDestination.Location = new System.Drawing.Point(400, 115);
            this.buttonDestination.Name = "buttonDestination";
            this.buttonDestination.Size = new System.Drawing.Size(26, 26);
            this.buttonDestination.TabIndex = 49;
            this.buttonDestination.Text = "...";
            this.buttonDestination.UseVisualStyleBackColor = true;
            this.buttonDestination.Click += new System.EventHandler(this.buttonDestination_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(363, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Smartphone Destination Folder (e.g. O:\\ViewRanger\\MapCache\\_PalbTN):";
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Location = new System.Drawing.Point(16, 119);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.Size = new System.Drawing.Size(365, 20);
            this.textBoxDestination.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Convert .png into";
            // 
            // textBoxPNG
            // 
            this.textBoxPNG.Location = new System.Drawing.Point(16, 172);
            this.textBoxPNG.Name = "textBoxPNG";
            this.textBoxPNG.Size = new System.Drawing.Size(85, 20);
            this.textBoxPNG.TabIndex = 45;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 581);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "ViewToolRangerV0.0.0.3";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMACFolder;
        private System.Windows.Forms.Button buttonMAC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPNG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.Button buttonDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAtlas;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonProcessGPX;
        private System.Windows.Forms.Button buttonExit2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOutputGPX;
        private System.Windows.Forms.TextBox textBoxInputGPX;
        private System.Windows.Forms.Button buttonInputGPXSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxVoiceFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxVoiceExtension;
        private System.Windows.Forms.CheckBox checkBoxReverseRoutes;
        private System.Windows.Forms.CheckBox checkBoxNewVoice;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonClear2;
        private System.Windows.Forms.CheckBox checkBoxRenumber;
    }
}

