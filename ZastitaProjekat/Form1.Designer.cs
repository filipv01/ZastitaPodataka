namespace ZastitaProjekat
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
            fileSystemWatcher1 = new FileSystemWatcher();
            textBox1 = new TextBox();
            Create = new Button();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            richTextBox2 = new RichTextBox();
            button2 = new Button();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            button4 = new Button();
            label3 = new Label();
            richTextBox3 = new RichTextBox();
            panel1 = new Panel();
            checkBox1 = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            label6 = new Label();
            button1 = new Button();
            dataGridView2 = new DataGridView();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.Filter = "*.txt*";
            fileSystemWatcher1.IncludeSubdirectories = true;
            fileSystemWatcher1.Path = "C:\\Users\\nis70\\OneDrive\\Desktop\\FileWatcher";
            fileSystemWatcher1.SynchronizingObject = this;
            fileSystemWatcher1.Changed += fileSystemWatcher1_Changed;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(61, 97);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(495, 39);
            textBox1.TabIndex = 0;
            // 
            // Create
            // 
            Create.Location = new Point(460, 877);
            Create.Name = "Create";
            Create.Size = new Size(174, 53);
            Create.TabIndex = 1;
            Create.Text = "Create";
            Create.UseVisualStyleBackColor = true;
            Create.Click += Create_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.EnableAutoDragDrop = true;
            richTextBox1.Location = new Point(460, 298);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(496, 502);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 40);
            label1.Name = "label1";
            label1.Size = new Size(127, 32);
            label1.TabIndex = 3;
            label1.Text = "File Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 243);
            label2.Name = "label2";
            label2.Size = new Size(66, 32);
            label2.TabIndex = 4;
            label2.Text = "Files:";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(1195, 303);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(489, 497);
            richTextBox2.TabIndex = 5;
            richTextBox2.Text = "";
            richTextBox2.TextChanged += richTextBox2_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(806, 90);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 7;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(61, 884);
            button3.Name = "button3";
            button3.Size = new Size(150, 46);
            button3.TabIndex = 9;
            button3.Text = "Show";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(61, 303);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.RowTemplate.Height = 41;
            dataGridView1.Size = new Size(335, 497);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // button4
            // 
            button4.Location = new Point(782, 877);
            button4.Name = "button4";
            button4.Size = new Size(174, 53);
            button4.TabIndex = 11;
            button4.Text = "Update";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(460, 235);
            label3.Name = "label3";
            label3.Size = new Size(62, 32);
            label3.TabIndex = 13;
            label3.Text = "Text:";
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new Point(1729, 303);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.ReadOnly = true;
            richTextBox3.Size = new Size(489, 497);
            richTextBox3.TabIndex = 14;
            richTextBox3.Text = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Location = new Point(1082, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(40, 941);
            panel1.TabIndex = 15;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(1729, 845);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(103, 36);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "Crypt";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1195, 243);
            label4.Name = "label4";
            label4.Size = new Size(114, 32);
            label4.TabIndex = 17;
            label4.Text = "Sent text:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1729, 243);
            label5.Name = "label5";
            label5.Size = new Size(150, 32);
            label5.TabIndex = 18;
            label5.Text = "Crypted text:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "TEA", "LEA" });
            comboBox1.Location = new Point(714, 235);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(242, 40);
            comboBox1.TabIndex = 19;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(714, 182);
            label6.Name = "label6";
            label6.Size = new Size(95, 32);
            label6.TabIndex = 20;
            label6.Text = "Cypher:";
            // 
            // button1
            // 
            button1.Location = new Point(246, 884);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 21;
            button1.Text = "Send file";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(2314, 303);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 82;
            dataGridView2.RowTemplate.Height = 41;
            dataGridView2.Size = new Size(335, 497);
            dataGridView2.TabIndex = 22;
            dataGridView2.CellDoubleClick += dataGridView2_CellDoubleClick;
            // 
            // button5
            // 
            button5.Location = new Point(2499, 884);
            button5.Name = "button5";
            button5.Size = new Size(150, 46);
            button5.TabIndex = 23;
            button5.Text = "Show";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(2675, 982);
            Controls.Add(button5);
            Controls.Add(dataGridView2);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(checkBox1);
            Controls.Add(panel1);
            Controls.Add(richTextBox3);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(dataGridView1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(richTextBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(Create);
            Controls.Add(textBox1);
            ForeColor = SystemColors.InactiveCaptionText;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FileSystemWatcher fileSystemWatcher1;
        private RichTextBox richTextBox1;
        private Button Create;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private RichTextBox richTextBox2;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView1;
        private Button button4;
        private Label label3;
        private RichTextBox richTextBox3;
        private Panel panel1;
        private CheckBox checkBox1;
        private Label label5;
        private Label label4;
        private ComboBox comboBox1;
        private Label label6;
        private Button button1;
        private Button button5;
        private DataGridView dataGridView2;
    }
}