﻿namespace robotTest
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Conn_gv = new System.Windows.Forms.DataGridView();
            this.Connect = new System.Windows.Forms.Button();
            this.log_rt = new System.Windows.Forms.RichTextBox();
            this.Script_gv = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Stop_bt = new System.Windows.Forms.Button();
            this.Delete_bt = new System.Windows.Forms.Button();
            this.Down_bt = new System.Windows.Forms.Button();
            this.Up_bt = new System.Windows.Forms.Button();
            this.LoadScript_bt = new System.Windows.Forms.Button();
            this.SaveScript_bt = new System.Windows.Forms.Button();
            this.ExcuteCommand_bt = new System.Windows.Forms.Button();
            this.RunScript_bt = new System.Windows.Forms.Button();
            this.AddToScript_bt = new System.Windows.Forms.Button();
            this.param_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Controller_cb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Instruc_cb = new System.Windows.Forms.ComboBox();
            this.CmdType_cb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DeviceNo_cb = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ProcessStart_bt = new System.Windows.Forms.Button();
            this.DataReq_bt = new System.Windows.Forms.Button();
            this.LDCM_bt = new System.Windows.Forms.Button();
            this.Port1_gv = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Port2_gv = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ALComplete_bt = new System.Windows.Forms.Button();
            this.Aligner_gv = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Conn_gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Script_gv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port1_gv)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Port2_gv)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Aligner_gv)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Conn_gv);
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Location = new System.Drawing.Point(825, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 259);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controller";
            // 
            // Conn_gv
            // 
            this.Conn_gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Conn_gv.Location = new System.Drawing.Point(11, 21);
            this.Conn_gv.MultiSelect = false;
            this.Conn_gv.Name = "Conn_gv";
            this.Conn_gv.ReadOnly = true;
            this.Conn_gv.RowHeadersVisible = false;
            this.Conn_gv.RowTemplate.Height = 24;
            this.Conn_gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Conn_gv.Size = new System.Drawing.Size(739, 204);
            this.Conn_gv.TabIndex = 10;
            this.Conn_gv.DataSourceChanged += new System.EventHandler(this.Conn_gv_DataSourceChanged);
            this.Conn_gv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Conn_gv_CellDoubleClick);
            this.Conn_gv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Conn_gv_CellFormatting);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(659, 230);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(91, 23);
            this.Connect.TabIndex = 9;
            this.Connect.Text = "Connect All";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // log_rt
            // 
            this.log_rt.Location = new System.Drawing.Point(825, 276);
            this.log_rt.Name = "log_rt";
            this.log_rt.Size = new System.Drawing.Size(748, 496);
            this.log_rt.TabIndex = 8;
            this.log_rt.Text = "";
            // 
            // Script_gv
            // 
            this.Script_gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Script_gv.Location = new System.Drawing.Point(5, 343);
            this.Script_gv.Name = "Script_gv";
            this.Script_gv.RowTemplate.Height = 24;
            this.Script_gv.Size = new System.Drawing.Size(788, 384);
            this.Script_gv.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Stop_bt);
            this.groupBox2.Controls.Add(this.Delete_bt);
            this.groupBox2.Controls.Add(this.Down_bt);
            this.groupBox2.Controls.Add(this.Up_bt);
            this.groupBox2.Controls.Add(this.LoadScript_bt);
            this.groupBox2.Controls.Add(this.SaveScript_bt);
            this.groupBox2.Controls.Add(this.ExcuteCommand_bt);
            this.groupBox2.Controls.Add(this.RunScript_bt);
            this.groupBox2.Controls.Add(this.AddToScript_bt);
            this.groupBox2.Controls.Add(this.param_tb);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Controller_cb);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Instruc_cb);
            this.groupBox2.Controls.Add(this.CmdType_cb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DeviceNo_cb);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 331);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // Stop_bt
            // 
            this.Stop_bt.Location = new System.Drawing.Point(91, 262);
            this.Stop_bt.Name = "Stop_bt";
            this.Stop_bt.Size = new System.Drawing.Size(75, 23);
            this.Stop_bt.TabIndex = 18;
            this.Stop_bt.Text = "Stop Script";
            this.Stop_bt.UseVisualStyleBackColor = true;
            this.Stop_bt.Click += new System.EventHandler(this.Stop_bt_Click);
            // 
            // Delete_bt
            // 
            this.Delete_bt.Location = new System.Drawing.Point(91, 301);
            this.Delete_bt.Name = "Delete_bt";
            this.Delete_bt.Size = new System.Drawing.Size(75, 23);
            this.Delete_bt.TabIndex = 17;
            this.Delete_bt.Text = "Delete";
            this.Delete_bt.UseVisualStyleBackColor = true;
            this.Delete_bt.Click += new System.EventHandler(this.Delete_bt_Click);
            // 
            // Down_bt
            // 
            this.Down_bt.Location = new System.Drawing.Point(380, 302);
            this.Down_bt.Name = "Down_bt";
            this.Down_bt.Size = new System.Drawing.Size(27, 23);
            this.Down_bt.TabIndex = 16;
            this.Down_bt.Text = "↓";
            this.Down_bt.UseVisualStyleBackColor = true;
            this.Down_bt.Click += new System.EventHandler(this.Down_bt_Click);
            // 
            // Up_bt
            // 
            this.Up_bt.Location = new System.Drawing.Point(347, 302);
            this.Up_bt.Name = "Up_bt";
            this.Up_bt.Size = new System.Drawing.Size(27, 23);
            this.Up_bt.TabIndex = 15;
            this.Up_bt.Text = "↑";
            this.Up_bt.UseVisualStyleBackColor = true;
            this.Up_bt.Click += new System.EventHandler(this.Up_bt_Click);
            // 
            // LoadScript_bt
            // 
            this.LoadScript_bt.Location = new System.Drawing.Point(266, 302);
            this.LoadScript_bt.Name = "LoadScript_bt";
            this.LoadScript_bt.Size = new System.Drawing.Size(75, 23);
            this.LoadScript_bt.TabIndex = 14;
            this.LoadScript_bt.Text = "Load";
            this.LoadScript_bt.UseVisualStyleBackColor = true;
            this.LoadScript_bt.Click += new System.EventHandler(this.LoadScript_bt_Click);
            // 
            // SaveScript_bt
            // 
            this.SaveScript_bt.Location = new System.Drawing.Point(185, 302);
            this.SaveScript_bt.Name = "SaveScript_bt";
            this.SaveScript_bt.Size = new System.Drawing.Size(75, 23);
            this.SaveScript_bt.TabIndex = 13;
            this.SaveScript_bt.Text = "Save";
            this.SaveScript_bt.UseVisualStyleBackColor = true;
            this.SaveScript_bt.Click += new System.EventHandler(this.SaveScript_bt_Click);
            // 
            // ExcuteCommand_bt
            // 
            this.ExcuteCommand_bt.Location = new System.Drawing.Point(10, 220);
            this.ExcuteCommand_bt.Name = "ExcuteCommand_bt";
            this.ExcuteCommand_bt.Size = new System.Drawing.Size(102, 23);
            this.ExcuteCommand_bt.TabIndex = 12;
            this.ExcuteCommand_bt.Text = "Excute Command";
            this.ExcuteCommand_bt.UseVisualStyleBackColor = true;
            this.ExcuteCommand_bt.Click += new System.EventHandler(this.ExcuteCommand_bt_Click);
            // 
            // RunScript_bt
            // 
            this.RunScript_bt.Location = new System.Drawing.Point(10, 262);
            this.RunScript_bt.Name = "RunScript_bt";
            this.RunScript_bt.Size = new System.Drawing.Size(75, 23);
            this.RunScript_bt.TabIndex = 11;
            this.RunScript_bt.Text = "Run Script";
            this.RunScript_bt.UseVisualStyleBackColor = true;
            this.RunScript_bt.Click += new System.EventHandler(this.RunScript_bt_Click);
            // 
            // AddToScript_bt
            // 
            this.AddToScript_bt.Location = new System.Drawing.Point(10, 301);
            this.AddToScript_bt.Name = "AddToScript_bt";
            this.AddToScript_bt.Size = new System.Drawing.Size(75, 23);
            this.AddToScript_bt.TabIndex = 10;
            this.AddToScript_bt.Text = "Add to Script";
            this.AddToScript_bt.UseVisualStyleBackColor = true;
            this.AddToScript_bt.Click += new System.EventHandler(this.AddToScript_bt_Click);
            // 
            // param_tb
            // 
            this.param_tb.Location = new System.Drawing.Point(79, 172);
            this.param_tb.Name = "param_tb";
            this.param_tb.Size = new System.Drawing.Size(147, 22);
            this.param_tb.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "參數";
            // 
            // Controller_cb
            // 
            this.Controller_cb.FormattingEnabled = true;
            this.Controller_cb.Location = new System.Drawing.Point(79, 19);
            this.Controller_cb.Name = "Controller_cb";
            this.Controller_cb.Size = new System.Drawing.Size(147, 20);
            this.Controller_cb.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "控制";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "指令";
            // 
            // Instruc_cb
            // 
            this.Instruc_cb.FormattingEnabled = true;
            this.Instruc_cb.Location = new System.Drawing.Point(79, 134);
            this.Instruc_cb.Name = "Instruc_cb";
            this.Instruc_cb.Size = new System.Drawing.Size(72, 20);
            this.Instruc_cb.TabIndex = 4;
            this.Instruc_cb.SelectedIndexChanged += new System.EventHandler(this.Instruc_cb_SelectedIndexChanged);
            // 
            // CmdType_cb
            // 
            this.CmdType_cb.FormattingEnabled = true;
            this.CmdType_cb.Location = new System.Drawing.Point(79, 92);
            this.CmdType_cb.Name = "CmdType_cb";
            this.CmdType_cb.Size = new System.Drawing.Size(72, 20);
            this.CmdType_cb.TabIndex = 3;
            this.CmdType_cb.SelectedIndexChanged += new System.EventHandler(this.CmdType_cb_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "命令種類";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DeviceNo";
            // 
            // DeviceNo_cb
            // 
            this.DeviceNo_cb.FormattingEnabled = true;
            this.DeviceNo_cb.Location = new System.Drawing.Point(79, 52);
            this.DeviceNo_cb.Name = "DeviceNo_cb";
            this.DeviceNo_cb.Size = new System.Drawing.Size(72, 20);
            this.DeviceNo_cb.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ProcessStart_bt);
            this.groupBox3.Controls.Add(this.DataReq_bt);
            this.groupBox3.Controls.Add(this.LDCM_bt);
            this.groupBox3.Controls.Add(this.Port1_gv);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 353);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Port1";
            // 
            // ProcessStart_bt
            // 
            this.ProcessStart_bt.Location = new System.Drawing.Point(171, 323);
            this.ProcessStart_bt.Name = "ProcessStart_bt";
            this.ProcessStart_bt.Size = new System.Drawing.Size(75, 23);
            this.ProcessStart_bt.TabIndex = 15;
            this.ProcessStart_bt.Text = "Start";
            this.ProcessStart_bt.UseVisualStyleBackColor = true;
            this.ProcessStart_bt.Click += new System.EventHandler(this.ProcessStart_bt_Click);
            // 
            // DataReq_bt
            // 
            this.DataReq_bt.Location = new System.Drawing.Point(89, 323);
            this.DataReq_bt.Name = "DataReq_bt";
            this.DataReq_bt.Size = new System.Drawing.Size(75, 23);
            this.DataReq_bt.TabIndex = 14;
            this.DataReq_bt.Text = "DATA REQ";
            this.DataReq_bt.UseVisualStyleBackColor = true;
            this.DataReq_bt.Click += new System.EventHandler(this.DataReq_bt_Click);
            // 
            // LDCM_bt
            // 
            this.LDCM_bt.Location = new System.Drawing.Point(7, 323);
            this.LDCM_bt.Name = "LDCM_bt";
            this.LDCM_bt.Size = new System.Drawing.Size(75, 23);
            this.LDCM_bt.TabIndex = 13;
            this.LDCM_bt.Text = "LDCM";
            this.LDCM_bt.UseVisualStyleBackColor = true;
            this.LDCM_bt.Click += new System.EventHandler(this.LDCM_bt_Click);
            // 
            // Port1_gv
            // 
            this.Port1_gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Port1_gv.Enabled = false;
            this.Port1_gv.Location = new System.Drawing.Point(6, 24);
            this.Port1_gv.Name = "Port1_gv";
            this.Port1_gv.RowHeadersVisible = false;
            this.Port1_gv.RowTemplate.Height = 24;
            this.Port1_gv.Size = new System.Drawing.Size(279, 292);
            this.Port1_gv.TabIndex = 12;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Port2_gv);
            this.groupBox4.Location = new System.Drawing.Point(6, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(291, 353);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Port2";
            // 
            // Port2_gv
            // 
            this.Port2_gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Port2_gv.Enabled = false;
            this.Port2_gv.Location = new System.Drawing.Point(6, 19);
            this.Port2_gv.Name = "Port2_gv";
            this.Port2_gv.RowHeadersVisible = false;
            this.Port2_gv.RowTemplate.Height = 24;
            this.Port2_gv.Size = new System.Drawing.Size(279, 328);
            this.Port2_gv.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 759);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(799, 733);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "自動模式";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.ALComplete_bt);
            this.groupBox5.Controls.Add(this.Aligner_gv);
            this.groupBox5.Location = new System.Drawing.Point(502, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(291, 353);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Aligner";
            // 
            // ALComplete_bt
            // 
            this.ALComplete_bt.Location = new System.Drawing.Point(7, 322);
            this.ALComplete_bt.Name = "ALComplete_bt";
            this.ALComplete_bt.Size = new System.Drawing.Size(75, 23);
            this.ALComplete_bt.TabIndex = 1;
            this.ALComplete_bt.Text = "Complete";
            this.ALComplete_bt.UseVisualStyleBackColor = true;
            this.ALComplete_bt.Click += new System.EventHandler(this.ALComplete_bt_Click);
            // 
            // Aligner_gv
            // 
            this.Aligner_gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Aligner_gv.Enabled = false;
            this.Aligner_gv.Location = new System.Drawing.Point(7, 22);
            this.Aligner_gv.Name = "Aligner_gv";
            this.Aligner_gv.RowHeadersVisible = false;
            this.Aligner_gv.RowTemplate.Height = 24;
            this.Aligner_gv.Size = new System.Drawing.Size(278, 294);
            this.Aligner_gv.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.Script_gv);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(799, 733);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "跑Script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "label6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1591, 785);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.log_rt);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Conn_gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Script_gv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Port1_gv)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Port2_gv)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Aligner_gv)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.RichTextBox log_rt;
        private System.Windows.Forms.DataGridView Script_gv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView Conn_gv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DeviceNo_cb;
        private System.Windows.Forms.ComboBox CmdType_cb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Instruc_cb;
        private System.Windows.Forms.ComboBox Controller_cb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ExcuteCommand_bt;
        private System.Windows.Forms.Button RunScript_bt;
        private System.Windows.Forms.Button AddToScript_bt;
        private System.Windows.Forms.TextBox param_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveScript_bt;
        private System.Windows.Forms.Button LoadScript_bt;
        private System.Windows.Forms.Button Down_bt;
        private System.Windows.Forms.Button Up_bt;
        private System.Windows.Forms.Button Delete_bt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView Port1_gv;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView Port2_gv;
        private System.Windows.Forms.Button Stop_bt;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView Aligner_gv;
        private System.Windows.Forms.Button ProcessStart_bt;
        private System.Windows.Forms.Button DataReq_bt;
        private System.Windows.Forms.Button LDCM_bt;
        private System.Windows.Forms.Button ALComplete_bt;
        private System.Windows.Forms.Label label6;
    }
}

