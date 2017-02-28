namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(322, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "请选择输入目录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(859, 102);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "开始";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(393, 30);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(446, 21);
            this.textBox11.TabIndex = 3;
            this.textBox11.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 74);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(322, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "请选择输出目录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(393, 74);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(446, 21);
            this.textBox22.TabIndex = 5;
            this.textBox22.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(2, 131);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(958, 439);
            this.textBoxLog.TabIndex = 6;
            this.textBoxLog.TextChanged += new System.EventHandler(this.textBoxLog_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 571);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.textBox22);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBoxLog;


    }
}

