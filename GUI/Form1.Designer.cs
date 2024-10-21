namespace GUI
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
            labelPodd = new Label();
            btnLaggTill = new Button();
            btnAndra = new Button();
            btnTaBort = new Button();
            textURL = new TextBox();
            comboBox1 = new ComboBox();
            labelURL = new Label();
            btnAterstall = new Button();
            textNamn = new TextBox();
            cbxKategori = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            listBoxKategori = new ListBox();
            richTextBeskrivning = new RichTextBox();
            listBoxAvsnitt = new ListBox();
            listPodd = new ListView();
            namn = new ColumnHeader();
            antalAvsnitt = new ColumnHeader();
            titel = new ColumnHeader();
            kategori = new ColumnHeader();
            label3 = new Label();
            SuspendLayout();
            // 
            // labelPodd
            // 
            labelPodd.AutoSize = true;
            labelPodd.Location = new Point(623, 49);
            labelPodd.Name = "labelPodd";
            labelPodd.Size = new Size(60, 20);
            labelPodd.TabIndex = 0;
            labelPodd.Text = "Podcast";
            // 
            // btnLaggTill
            // 
            btnLaggTill.Location = new Point(411, 151);
            btnLaggTill.Margin = new Padding(3, 4, 3, 4);
            btnLaggTill.Name = "btnLaggTill";
            btnLaggTill.Size = new Size(77, 28);
            btnLaggTill.TabIndex = 1;
            btnLaggTill.Text = "Lägg till";
            btnLaggTill.UseVisualStyleBackColor = true;
            btnLaggTill.Click += btnLaggTill_Click;
            // 
            // btnAndra
            // 
            btnAndra.Location = new Point(495, 151);
            btnAndra.Margin = new Padding(3, 4, 3, 4);
            btnAndra.Name = "btnAndra";
            btnAndra.Size = new Size(77, 28);
            btnAndra.TabIndex = 2;
            btnAndra.Text = "Ändra";
            btnAndra.UseVisualStyleBackColor = true;
            btnAndra.Click += btnAndra_Click;
            // 
            // btnTaBort
            // 
            btnTaBort.Location = new Point(578, 151);
            btnTaBort.Margin = new Padding(3, 4, 3, 4);
            btnTaBort.Name = "btnTaBort";
            btnTaBort.Size = new Size(77, 28);
            btnTaBort.TabIndex = 3;
            btnTaBort.Text = "Ta bort";
            btnTaBort.UseVisualStyleBackColor = true;
            // 
            // textURL
            // 
            textURL.Location = new Point(435, 187);
            textURL.Margin = new Padding(3, 4, 3, 4);
            textURL.Name = "textURL";
            textURL.Size = new Size(219, 27);
            textURL.TabIndex = 4;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(411, 112);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(159, 28);
            comboBox1.TabIndex = 5;
            // 
            // labelURL
            // 
            labelURL.AutoSize = true;
            labelURL.Location = new Point(385, 191);
            labelURL.Name = "labelURL";
            labelURL.Size = new Size(35, 20);
            labelURL.TabIndex = 6;
            labelURL.Text = "URL";
            // 
            // btnAterstall
            // 
            btnAterstall.Location = new Point(578, 112);
            btnAterstall.Margin = new Padding(3, 4, 3, 4);
            btnAterstall.Name = "btnAterstall";
            btnAterstall.Size = new Size(77, 28);
            btnAterstall.TabIndex = 7;
            btnAterstall.Text = "Återställ";
            btnAterstall.UseVisualStyleBackColor = true;
            // 
            // textNamn
            // 
            textNamn.Location = new Point(80, 148);
            textNamn.Margin = new Padding(3, 4, 3, 4);
            textNamn.Name = "textNamn";
            textNamn.Size = new Size(219, 27);
            textNamn.TabIndex = 8;
            // 
            // cbxKategori
            // 
            cbxKategori.FormattingEnabled = true;
            cbxKategori.Location = new Point(82, 187);
            cbxKategori.Margin = new Padding(3, 4, 3, 4);
            cbxKategori.Name = "cbxKategori";
            cbxKategori.Size = new Size(217, 28);
            cbxKategori.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(847, 191);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 11;
            label1.Text = "Avsnitt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1109, 191);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 12;
            label2.Text = "Kategori";
            // 
            // button1
            // 
            button1.Location = new Point(1177, 216);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(77, 28);
            button1.TabIndex = 15;
            button1.Text = "Ta bort";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(1094, 216);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(77, 28);
            button2.TabIndex = 14;
            button2.Text = "Ändra";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(1010, 216);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(77, 28);
            button3.TabIndex = 13;
            button3.Text = "Lägg till";
            button3.UseVisualStyleBackColor = true;
            // 
            // listBoxKategori
            // 
            listBoxKategori.FormattingEnabled = true;
            listBoxKategori.Location = new Point(1010, 252);
            listBoxKategori.Margin = new Padding(3, 4, 3, 4);
            listBoxKategori.Name = "listBoxKategori";
            listBoxKategori.Size = new Size(243, 144);
            listBoxKategori.TabIndex = 16;
            // 
            // richTextBeskrivning
            // 
            richTextBeskrivning.Location = new Point(1010, 405);
            richTextBeskrivning.Margin = new Padding(3, 4, 3, 4);
            richTextBeskrivning.Name = "richTextBeskrivning";
            richTextBeskrivning.Size = new Size(243, 203);
            richTextBeskrivning.TabIndex = 17;
            richTextBeskrivning.Text = "";
            // 
            // listBoxAvsnitt
            // 
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.Location = new Point(773, 252);
            listBoxAvsnitt.Margin = new Padding(3, 4, 3, 4);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(215, 364);
            listBoxAvsnitt.TabIndex = 18;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            // 
            // listPodd
            // 
            listPodd.Columns.AddRange(new ColumnHeader[] { namn, antalAvsnitt, titel, kategori });
            listPodd.Location = new Point(80, 252);
            listPodd.Margin = new Padding(3, 4, 3, 4);
            listPodd.Name = "listPodd";
            listPodd.Size = new Size(631, 364);
            listPodd.TabIndex = 19;
            listPodd.UseCompatibleStateImageBehavior = false;
            listPodd.View = View.Details;
            listPodd.SelectedIndexChanged += listPodd_SelectedIndexChanged;
            // 
            // namn
            // 
            namn.Text = "Namn";
            namn.Width = 225;
            // 
            // antalAvsnitt
            // 
            antalAvsnitt.Text = "Antal Avsnitt";
            antalAvsnitt.Width = 120;
            // 
            // titel
            // 
            titel.Text = "Titel";
            // 
            // kategori
            // 
            kategori.Text = "Kategori";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 115);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 20;
            label3.Text = "label3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1336, 793);
            Controls.Add(label3);
            Controls.Add(listPodd);
            Controls.Add(listBoxAvsnitt);
            Controls.Add(richTextBeskrivning);
            Controls.Add(listBoxKategori);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbxKategori);
            Controls.Add(textNamn);
            Controls.Add(btnAterstall);
            Controls.Add(labelURL);
            Controls.Add(comboBox1);
            Controls.Add(textURL);
            Controls.Add(btnTaBort);
            Controls.Add(btnAndra);
            Controls.Add(btnLaggTill);
            Controls.Add(labelPodd);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPodd;
        private Button btnLaggTill;
        private Button btnAndra;
        private Button btnTaBort;
        private TextBox textURL;
        private ComboBox comboBox1;
        private Label labelURL;
        private Button btnAterstall;
        private TextBox textNamn;
        private ComboBox cbxKategori;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
        private ListBox listBoxKategori;
        private RichTextBox richTextBeskrivning;
        private ListBox listBoxAvsnitt;
        private ListView listPodd;
        private ColumnHeader antalAvsnitt;
        private ColumnHeader namn;
        private ColumnHeader titel;
        private Label label3;
        private ColumnHeader kategori;
    }
}
