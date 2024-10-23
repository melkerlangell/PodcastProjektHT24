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
            comboBoxFiltrera = new ComboBox();
            labelURL = new Label();
            btnAterstall = new Button();
            textNamn = new TextBox();
            cbxKategori = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            DeleteKategori = new Button();
            EditKategori = new Button();
            AddKategori = new Button();
            listBoxKategori = new ListBox();
            richTextBeskrivning = new RichTextBox();
            listBoxAvsnitt = new ListBox();
            listPodd = new ListView();
            namn = new ColumnHeader();
            antalAvsnitt = new ColumnHeader();
            titel = new ColumnHeader();
            kategori = new ColumnHeader();
            label3 = new Label();
            textBoxKategori = new TextBox();
            labelKategori = new Label();
            SuspendLayout();
            // 
            // labelPodd
            // 
            labelPodd.AutoSize = true;
            labelPodd.Location = new Point(545, 37);
            labelPodd.Name = "labelPodd";
            labelPodd.Size = new Size(49, 15);
            labelPodd.TabIndex = 0;
            labelPodd.Text = "Podcast";
            // 
            // btnLaggTill
            // 
            btnLaggTill.Location = new Point(360, 113);
            btnLaggTill.Name = "btnLaggTill";
            btnLaggTill.Size = new Size(67, 21);
            btnLaggTill.TabIndex = 1;
            btnLaggTill.Text = "Lägg till";
            btnLaggTill.UseVisualStyleBackColor = true;
            btnLaggTill.Click += btnLaggTill_Click;
            // 
            // btnAndra
            // 
            btnAndra.Location = new Point(433, 113);
            btnAndra.Name = "btnAndra";
            btnAndra.Size = new Size(67, 21);
            btnAndra.TabIndex = 2;
            btnAndra.Text = "Ändra";
            btnAndra.UseVisualStyleBackColor = true;
            btnAndra.Click += btnAndra_Click_1;
            // 
            // btnTaBort
            // 
            btnTaBort.Location = new Point(506, 113);
            btnTaBort.Name = "btnTaBort";
            btnTaBort.Size = new Size(67, 21);
            btnTaBort.TabIndex = 3;
            btnTaBort.Text = "Ta bort";
            btnTaBort.UseVisualStyleBackColor = true;
            btnTaBort.Click += btnTaBort_Click;
            // 
            // textURL
            // 
            textURL.Location = new Point(381, 140);
            textURL.Name = "textURL";
            textURL.Size = new Size(192, 23);
            textURL.TabIndex = 4;
            // 
            // comboBoxFiltrera
            // 
            comboBoxFiltrera.FormattingEnabled = true;
            comboBoxFiltrera.Location = new Point(360, 84);
            comboBoxFiltrera.Name = "comboBoxFiltrera";
            comboBoxFiltrera.Size = new Size(140, 23);
            comboBoxFiltrera.TabIndex = 5;
            comboBoxFiltrera.Text = "filtrera...";
            comboBoxFiltrera.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // labelURL
            // 
            labelURL.AutoSize = true;
            labelURL.Location = new Point(337, 143);
            labelURL.Name = "labelURL";
            labelURL.Size = new Size(28, 15);
            labelURL.TabIndex = 6;
            labelURL.Text = "URL";
            // 
            // btnAterstall
            // 
            btnAterstall.Location = new Point(506, 84);
            btnAterstall.Name = "btnAterstall";
            btnAterstall.Size = new Size(67, 21);
            btnAterstall.TabIndex = 7;
            btnAterstall.Text = "Återställ";
            btnAterstall.UseVisualStyleBackColor = true;
            btnAterstall.Click += btnAterstall_Click;
            // 
            // textNamn
            // 
            textNamn.Location = new Point(70, 111);
            textNamn.Name = "textNamn";
            textNamn.Size = new Size(192, 23);
            textNamn.TabIndex = 8;
            // 
            // cbxKategori
            // 
            cbxKategori.FormattingEnabled = true;
            cbxKategori.Location = new Point(72, 140);
            cbxKategori.Name = "cbxKategori";
            cbxKategori.Size = new Size(190, 23);
            cbxKategori.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(741, 143);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 11;
            label1.Text = "Avsnitt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(973, 119);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 12;
            label2.Text = "Kategori";
            // 
            // DeleteKategori
            // 
            DeleteKategori.Location = new Point(1103, 265);
            DeleteKategori.Name = "DeleteKategori";
            DeleteKategori.Size = new Size(67, 21);
            DeleteKategori.TabIndex = 15;
            DeleteKategori.Text = "Ta bort";
            DeleteKategori.UseVisualStyleBackColor = true;
            DeleteKategori.Click += DeleteKategori_Click;
            // 
            // EditKategori
            // 
            EditKategori.Location = new Point(1103, 238);
            EditKategori.Name = "EditKategori";
            EditKategori.Size = new Size(67, 21);
            EditKategori.TabIndex = 14;
            EditKategori.Text = "Ändra";
            EditKategori.UseVisualStyleBackColor = true;
            EditKategori.Click += EditKategori_Click;
            // 
            // AddKategori
            // 
            AddKategori.Location = new Point(1103, 211);
            AddKategori.Name = "AddKategori";
            AddKategori.Size = new Size(67, 21);
            AddKategori.TabIndex = 13;
            AddKategori.Text = "Lägg till";
            AddKategori.UseVisualStyleBackColor = true;
            AddKategori.Click += AddKategori_Click;
            // 
            // listBoxKategori
            // 
            listBoxKategori.FormattingEnabled = true;
            listBoxKategori.ItemHeight = 15;
            listBoxKategori.Location = new Point(884, 189);
            listBoxKategori.Name = "listBoxKategori";
            listBoxKategori.Size = new Size(213, 109);
            listBoxKategori.TabIndex = 16;
            listBoxKategori.SelectedIndexChanged += listBoxKategori_SelectedIndexChanged;
            // 
            // richTextBeskrivning
            // 
            richTextBeskrivning.Location = new Point(884, 304);
            richTextBeskrivning.Name = "richTextBeskrivning";
            richTextBeskrivning.Size = new Size(213, 153);
            richTextBeskrivning.TabIndex = 17;
            richTextBeskrivning.Text = "";
            // 
            // listBoxAvsnitt
            // 
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.ItemHeight = 15;
            listBoxAvsnitt.Location = new Point(676, 189);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(189, 274);
            listBoxAvsnitt.TabIndex = 18;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            // 
            // listPodd
            // 
            listPodd.Columns.AddRange(new ColumnHeader[] { namn, antalAvsnitt, titel, kategori });
            listPodd.FullRowSelect = true;
            listPodd.Location = new Point(70, 189);
            listPodd.Name = "listPodd";
            listPodd.Size = new Size(553, 274);
            listPodd.TabIndex = 19;
            listPodd.UseCompatibleStateImageBehavior = false;
            listPodd.View = View.Details;
            listPodd.SelectedIndexChanged += listPodd_SelectedIndexChanged;
            // 
            // namn
            // 
            namn.Text = "Namn";
            namn.Width = 125;
            // 
            // antalAvsnitt
            // 
            antalAvsnitt.Text = "Antal Avsnitt";
            antalAvsnitt.Width = 120;
            // 
            // titel
            // 
            titel.Text = "Titel";
            titel.Width = 200;
            // 
            // kategori
            // 
            kategori.Text = "Kategori";
            kategori.Width = 100;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(72, 86);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 20;
            label3.Text = "Ange Namn:";
            // 
            // textBoxKategori
            // 
            textBoxKategori.Location = new Point(961, 160);
            textBoxKategori.Name = "textBoxKategori";
            textBoxKategori.Size = new Size(136, 23);
            textBoxKategori.TabIndex = 21;
            // 
            // labelKategori
            // 
            labelKategori.AutoSize = true;
            labelKategori.Location = new Point(884, 163);
            labelKategori.Name = "labelKategori";
            labelKategori.Size = new Size(71, 15);
            labelKategori.TabIndex = 22;
            labelKategori.Text = "Ny kategori:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 595);
            Controls.Add(labelKategori);
            Controls.Add(textBoxKategori);
            Controls.Add(label3);
            Controls.Add(listPodd);
            Controls.Add(listBoxAvsnitt);
            Controls.Add(richTextBeskrivning);
            Controls.Add(listBoxKategori);
            Controls.Add(DeleteKategori);
            Controls.Add(EditKategori);
            Controls.Add(AddKategori);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbxKategori);
            Controls.Add(textNamn);
            Controls.Add(btnAterstall);
            Controls.Add(labelURL);
            Controls.Add(comboBoxFiltrera);
            Controls.Add(textURL);
            Controls.Add(btnTaBort);
            Controls.Add(btnAndra);
            Controls.Add(btnLaggTill);
            Controls.Add(labelPodd);
            Name = "Form1";
            Text = "Podcast";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPodd;
        private Button btnLaggTill;
        private Button btnAndra;
        private Button btnTaBort;
        private TextBox textURL;
        private ComboBox comboBoxFiltrera;
        private Label labelURL;
        private Button btnAterstall;
        private TextBox textNamn;
        private ComboBox cbxKategori;
        private Label label1;
        private Label label2;
        private Button DeleteKategori;
        private Button EditKategori;
        private Button AddKategori;
        private ListBox listBoxKategori;
        private RichTextBox richTextBeskrivning;
        private ListBox listBoxAvsnitt;
        private ListView listPodd;
        private ColumnHeader antalAvsnitt;
        private ColumnHeader namn;
        private ColumnHeader titel;
        private Label label3;
        private ColumnHeader kategori;
        private TextBox textBoxKategori;
        private Label labelKategori;
    }
}
