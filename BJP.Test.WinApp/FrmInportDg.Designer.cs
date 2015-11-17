namespace BJP.Test.WinApp
{
    partial class FrmInportDg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInDir = new System.Windows.Forms.Label();
            this.btnSelInDir = new System.Windows.Forms.Button();
            this.txtInDir = new System.Windows.Forms.TextBox();
            this.lblInFile = new System.Windows.Forms.Label();
            this.txtInFile = new System.Windows.Forms.TextBox();
            this.btnSelInFile = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnInTime = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnCar = new System.Windows.Forms.Button();
            this.btnJP = new System.Windows.Forms.Button();
            this.btnInGd = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.btnBatchJp = new System.Windows.Forms.Button();
            this.btnShuiX = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInDir
            // 
            this.lblInDir.AutoSize = true;
            this.lblInDir.Location = new System.Drawing.Point(13, 13);
            this.lblInDir.Name = "lblInDir";
            this.lblInDir.Size = new System.Drawing.Size(65, 12);
            this.lblInDir.TabIndex = 0;
            this.lblInDir.Text = "输入目录：";
            // 
            // btnSelInDir
            // 
            this.btnSelInDir.Location = new System.Drawing.Point(387, 8);
            this.btnSelInDir.Name = "btnSelInDir";
            this.btnSelInDir.Size = new System.Drawing.Size(75, 23);
            this.btnSelInDir.TabIndex = 1;
            this.btnSelInDir.Text = "选择目录";
            this.btnSelInDir.UseVisualStyleBackColor = true;
            this.btnSelInDir.Click += new System.EventHandler(this.btnSelInDir_Click);
            // 
            // txtInDir
            // 
            this.txtInDir.Location = new System.Drawing.Point(84, 10);
            this.txtInDir.Name = "txtInDir";
            this.txtInDir.Size = new System.Drawing.Size(280, 21);
            this.txtInDir.TabIndex = 2;
            this.txtInDir.Text = "f:\\临时文件\\输入目录";
            // 
            // lblInFile
            // 
            this.lblInFile.AutoSize = true;
            this.lblInFile.Location = new System.Drawing.Point(15, 44);
            this.lblInFile.Name = "lblInFile";
            this.lblInFile.Size = new System.Drawing.Size(65, 12);
            this.lblInFile.TabIndex = 3;
            this.lblInFile.Text = "输入文件：";
            // 
            // txtInFile
            // 
            this.txtInFile.Location = new System.Drawing.Point(84, 34);
            this.txtInFile.Name = "txtInFile";
            this.txtInFile.Size = new System.Drawing.Size(280, 21);
            this.txtInFile.TabIndex = 4;
            // 
            // btnSelInFile
            // 
            this.btnSelInFile.Location = new System.Drawing.Point(387, 32);
            this.btnSelInFile.Name = "btnSelInFile";
            this.btnSelInFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelInFile.TabIndex = 5;
            this.btnSelInFile.Text = "选择文件";
            this.btnSelInFile.UseVisualStyleBackColor = true;
            this.btnSelInFile.Click += new System.EventHandler(this.btnSelInFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnInTime
            // 
            this.btnInTime.Location = new System.Drawing.Point(17, 376);
            this.btnInTime.Name = "btnInTime";
            this.btnInTime.Size = new System.Drawing.Size(75, 23);
            this.btnInTime.TabIndex = 6;
            this.btnInTime.Text = "导入运营时间";
            this.btnInTime.UseVisualStyleBackColor = true;
            this.btnInTime.Click += new System.EventHandler(this.btnInTime_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 106);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 21);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(17, 133);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(172, 21);
            this.textBox2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "转为度分秒";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(289, 103);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(173, 21);
            this.textBox3.TabIndex = 10;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(289, 132);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(173, 21);
            this.textBox4.TabIndex = 11;
            // 
            // btnCar
            // 
            this.btnCar.Location = new System.Drawing.Point(99, 375);
            this.btnCar.Name = "btnCar";
            this.btnCar.Size = new System.Drawing.Size(75, 23);
            this.btnCar.TabIndex = 12;
            this.btnCar.Text = "导入车辆 ";
            this.btnCar.UseVisualStyleBackColor = true;
            this.btnCar.Click += new System.EventHandler(this.btnCar_Click);
            // 
            // btnJP
            // 
            this.btnJP.Location = new System.Drawing.Point(196, 129);
            this.btnJP.Name = "btnJP";
            this.btnJP.Size = new System.Drawing.Size(75, 23);
            this.btnJP.TabIndex = 13;
            this.btnJP.Text = "纠偏";
            this.btnJP.UseVisualStyleBackColor = true;
            this.btnJP.Click += new System.EventHandler(this.btnJP_Click);
            // 
            // btnInGd
            // 
            this.btnInGd.Location = new System.Drawing.Point(196, 374);
            this.btnInGd.Name = "btnInGd";
            this.btnInGd.Size = new System.Drawing.Size(98, 23);
            this.btnInGd.TabIndex = 14;
            this.btnInGd.Text = "导入高德数据";
            this.btnInGd.UseVisualStyleBackColor = true;
            this.btnInGd.Click += new System.EventHandler(this.btnInGd_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(17, 344);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(277, 21);
            this.textBox5.TabIndex = 15;
            // 
            // btnBatchJp
            // 
            this.btnBatchJp.Location = new System.Drawing.Point(196, 159);
            this.btnBatchJp.Name = "btnBatchJp";
            this.btnBatchJp.Size = new System.Drawing.Size(75, 23);
            this.btnBatchJp.TabIndex = 16;
            this.btnBatchJp.Text = "批量纠偏";
            this.btnBatchJp.UseVisualStyleBackColor = true;
            this.btnBatchJp.Click += new System.EventHandler(this.btnBatchJp_Click);
            // 
            // btnShuiX
            // 
            this.btnShuiX.Location = new System.Drawing.Point(328, 374);
            this.btnShuiX.Name = "btnShuiX";
            this.btnShuiX.Size = new System.Drawing.Size(98, 23);
            this.btnShuiX.TabIndex = 17;
            this.btnShuiX.Text = "导入水乡数据";
            this.btnShuiX.UseVisualStyleBackColor = true;
            this.btnShuiX.Click += new System.EventHandler(this.btnShuiX_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(432, 374);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmInportDg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 411);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnShuiX);
            this.Controls.Add(this.btnBatchJp);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.btnInGd);
            this.Controls.Add(this.btnJP);
            this.Controls.Add(this.btnCar);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnInTime);
            this.Controls.Add(this.btnSelInFile);
            this.Controls.Add(this.txtInFile);
            this.Controls.Add(this.lblInFile);
            this.Controls.Add(this.txtInDir);
            this.Controls.Add(this.btnSelInDir);
            this.Controls.Add(this.lblInDir);
            this.Name = "FrmInportDg";
            this.Text = "基础数据导入处理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInDir;
        private System.Windows.Forms.Button btnSelInDir;
        private System.Windows.Forms.TextBox txtInDir;
        private System.Windows.Forms.Label lblInFile;
        private System.Windows.Forms.TextBox txtInFile;
        private System.Windows.Forms.Button btnSelInFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnInTime;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnCar;
        private System.Windows.Forms.Button btnJP;
        private System.Windows.Forms.Button btnInGd;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button btnBatchJp;
        private System.Windows.Forms.Button btnShuiX;
        private System.Windows.Forms.Button button2;
    }
}