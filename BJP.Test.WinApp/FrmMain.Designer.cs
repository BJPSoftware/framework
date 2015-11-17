namespace BJP.Test.WinApp
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogTest = new System.Windows.Forms.Button();
            this.txtInPath = new System.Windows.Forms.TextBox();
            this.btnDoFile = new System.Windows.Forms.Button();
            this.btnDirSelect = new System.Windows.Forms.Button();
            this.dialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pgbarFile = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnUdpServerTest = new System.Windows.Forms.Button();
            this.btnTcpClient = new System.Windows.Forms.Button();
            this.btn_TcpSend = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.doText = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.doSQL = new System.Windows.Forms.Button();
            this.doExcelFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDoData = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnOutDir = new System.Windows.Forms.Button();
            this.txtOutDir = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLogTest
            // 
            this.btnLogTest.Location = new System.Drawing.Point(871, 489);
            this.btnLogTest.Name = "btnLogTest";
            this.btnLogTest.Size = new System.Drawing.Size(90, 31);
            this.btnLogTest.TabIndex = 0;
            this.btnLogTest.Text = "日志测试";
            this.btnLogTest.UseVisualStyleBackColor = true;
            this.btnLogTest.Click += new System.EventHandler(this.btnLogTest_Click);
            // 
            // txtInPath
            // 
            this.txtInPath.Location = new System.Drawing.Point(13, 13);
            this.txtInPath.Name = "txtInPath";
            this.txtInPath.Size = new System.Drawing.Size(344, 21);
            this.txtInPath.TabIndex = 1;
            // 
            // btnDoFile
            // 
            this.btnDoFile.Location = new System.Drawing.Point(721, 489);
            this.btnDoFile.Name = "btnDoFile";
            this.btnDoFile.Size = new System.Drawing.Size(131, 33);
            this.btnDoFile.TabIndex = 2;
            this.btnDoFile.Text = "宜兴线路站点处理";
            this.btnDoFile.UseVisualStyleBackColor = true;
            this.btnDoFile.Click += new System.EventHandler(this.btnDoFile_Click);
            // 
            // btnDirSelect
            // 
            this.btnDirSelect.Location = new System.Drawing.Point(381, 10);
            this.btnDirSelect.Name = "btnDirSelect";
            this.btnDirSelect.Size = new System.Drawing.Size(75, 23);
            this.btnDirSelect.TabIndex = 3;
            this.btnDirSelect.Text = "输入目录";
            this.btnDirSelect.UseVisualStyleBackColor = true;
            this.btnDirSelect.Click += new System.EventHandler(this.btnDirSelect_Click);
            // 
            // pgbarFile
            // 
            this.pgbarFile.Location = new System.Drawing.Point(12, 134);
            this.pgbarFile.Name = "pgbarFile";
            this.pgbarFile.Size = new System.Drawing.Size(344, 23);
            this.pgbarFile.TabIndex = 4;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(381, 51);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 211);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(344, 21);
            this.textBox1.TabIndex = 6;
            // 
            // btnUdpServerTest
            // 
            this.btnUdpServerTest.Location = new System.Drawing.Point(608, 491);
            this.btnUdpServerTest.Name = "btnUdpServerTest";
            this.btnUdpServerTest.Size = new System.Drawing.Size(90, 31);
            this.btnUdpServerTest.TabIndex = 7;
            this.btnUdpServerTest.Text = "UdpServer测试";
            this.btnUdpServerTest.UseVisualStyleBackColor = true;
            this.btnUdpServerTest.Click += new System.EventHandler(this.btnUdpServerTest_Click);
            // 
            // btnTcpClient
            // 
            this.btnTcpClient.Location = new System.Drawing.Point(366, 209);
            this.btnTcpClient.Name = "btnTcpClient";
            this.btnTcpClient.Size = new System.Drawing.Size(89, 23);
            this.btnTcpClient.TabIndex = 8;
            this.btnTcpClient.Text = "TcpConnect";
            this.btnTcpClient.UseVisualStyleBackColor = true;
            this.btnTcpClient.Click += new System.EventHandler(this.btnTcpClient_Click);
            // 
            // btn_TcpSend
            // 
            this.btn_TcpSend.Location = new System.Drawing.Point(474, 209);
            this.btn_TcpSend.Name = "btn_TcpSend";
            this.btn_TcpSend.Size = new System.Drawing.Size(89, 23);
            this.btn_TcpSend.TabIndex = 9;
            this.btn_TcpSend.Text = "TcpSend";
            this.btn_TcpSend.UseVisualStyleBackColor = true;
            this.btn_TcpSend.Click += new System.EventHandler(this.btn_TcpSend_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 249);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(344, 21);
            this.textBox2.TabIndex = 10;
            // 
            // doText
            // 
            this.doText.Location = new System.Drawing.Point(366, 246);
            this.doText.Name = "doText";
            this.doText.Size = new System.Drawing.Size(75, 23);
            this.doText.TabIndex = 11;
            this.doText.Text = "doText";
            this.doText.UseVisualStyleBackColor = true;
            this.doText.Click += new System.EventHandler(this.doText_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(671, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(265, 460);
            this.listBox1.TabIndex = 12;
            // 
            // doSQL
            // 
            this.doSQL.Location = new System.Drawing.Point(460, 245);
            this.doSQL.Name = "doSQL";
            this.doSQL.Size = new System.Drawing.Size(75, 23);
            this.doSQL.TabIndex = 13;
            this.doSQL.Text = "doSQL";
            this.doSQL.UseVisualStyleBackColor = true;
            this.doSQL.Click += new System.EventHandler(this.doSQL_Click);
            // 
            // doExcelFile
            // 
            this.doExcelFile.Location = new System.Drawing.Point(561, 245);
            this.doExcelFile.Name = "doExcelFile";
            this.doExcelFile.Size = new System.Drawing.Size(88, 23);
            this.doExcelFile.TabIndex = 14;
            this.doExcelFile.Text = "doExcelFile";
            this.doExcelFile.UseVisualStyleBackColor = true;
            this.doExcelFile.Click += new System.EventHandler(this.doExcelFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDoData
            // 
            this.btnDoData.Location = new System.Drawing.Point(575, 10);
            this.btnDoData.Name = "btnDoData";
            this.btnDoData.Size = new System.Drawing.Size(75, 23);
            this.btnDoData.TabIndex = 15;
            this.btnDoData.Text = "数据处理";
            this.btnDoData.UseVisualStyleBackColor = true;
            this.btnDoData.Click += new System.EventHandler(this.btnDoData_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(381, 51);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 16;
            this.btnSelectFile.Text = "输入文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 51);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(344, 21);
            this.txtFileName.TabIndex = 17;
            // 
            // btnOutDir
            // 
            this.btnOutDir.Location = new System.Drawing.Point(381, 92);
            this.btnOutDir.Name = "btnOutDir";
            this.btnOutDir.Size = new System.Drawing.Size(75, 23);
            this.btnOutDir.TabIndex = 19;
            this.btnOutDir.Text = "输出目录";
            this.btnOutDir.UseVisualStyleBackColor = true;
            this.btnOutDir.Click += new System.EventHandler(this.btnOutDir_Click);
            // 
            // txtOutDir
            // 
            this.txtOutDir.Location = new System.Drawing.Point(13, 95);
            this.txtOutDir.Name = "txtOutDir";
            this.txtOutDir.Size = new System.Drawing.Size(344, 21);
            this.txtOutDir.TabIndex = 18;
            this.txtOutDir.Text = "f:\\临时文件\\";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 534);
            this.Controls.Add(this.btnOutDir);
            this.Controls.Add(this.txtOutDir);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnDoData);
            this.Controls.Add(this.doExcelFile);
            this.Controls.Add(this.doSQL);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.doText);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_TcpSend);
            this.Controls.Add(this.btnTcpClient);
            this.Controls.Add(this.btnUdpServerTest);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pgbarFile);
            this.Controls.Add(this.btnDirSelect);
            this.Controls.Add(this.btnDoFile);
            this.Controls.Add(this.txtInPath);
            this.Controls.Add(this.btnLogTest);
            this.Name = "FrmMain";
            this.Text = "测试窗体";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogTest;
        private System.Windows.Forms.TextBox txtInPath;
        private System.Windows.Forms.Button btnDoFile;
        private System.Windows.Forms.Button btnDirSelect;
        private System.Windows.Forms.FolderBrowserDialog dialogFolder;
        private System.Windows.Forms.ProgressBar pgbarFile;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnUdpServerTest;
        private System.Windows.Forms.Button btnTcpClient;
        private System.Windows.Forms.Button btn_TcpSend;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button doText;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button doSQL;
        private System.Windows.Forms.Button doExcelFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDoData;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnOutDir;
        private System.Windows.Forms.TextBox txtOutDir;
    }
}

