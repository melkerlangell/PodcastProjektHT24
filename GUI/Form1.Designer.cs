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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            labelValjKategori = new Label();
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
            Uppdateringsintervall = new ColumnHeader();
            label3 = new Label();
            textBoxKategori = new TextBox();
            labelKategori = new Label();
            labelPodd = new Label();
            labelBeskrivning = new Label();
            comboBoxIntervall = new ComboBox();
            labelIntervall = new Label();
            labelUppdatering = new Label();
            helpButton = new Button();
            listBoxUpd = new ListBox();
            SuspendLayout();
            // 
            // labelValjKategori
            // 
            labelValjKategori.AutoSize = true;
            labelValjKategori.Location = new Point(119, 132);
            labelValjKategori.Name = "labelValjKategori";
            labelValjKategori.Size = new Size(75, 15);
            labelValjKategori.TabIndex = 0;
            labelValjKategori.Text = "Välj Kategori:";
            // 
            // btnLaggTill
            // 
            btnLaggTill.Location = new Point(530, 126);
            btnLaggTill.Name = "btnLaggTill";
            btnLaggTill.Size = new Size(67, 21);
            btnLaggTill.TabIndex = 1;
            btnLaggTill.Text = "Lägg till";
            btnLaggTill.UseVisualStyleBackColor = true;
            btnLaggTill.Click += btnLaggTill_Click;
            // 
            // btnAndra
            // 
            btnAndra.Location = new Point(269, 187);
            btnAndra.Name = "btnAndra";
            btnAndra.Size = new Size(67, 21);
            btnAndra.TabIndex = 2;
            btnAndra.Text = "Ändra";
            btnAndra.UseVisualStyleBackColor = true;
            btnAndra.Click += btnAndra_Click_1;
            // 
            // btnTaBort
            // 
            btnTaBort.Location = new Point(614, 126);
            btnTaBort.Name = "btnTaBort";
            btnTaBort.Size = new Size(67, 21);
            btnTaBort.TabIndex = 3;
            btnTaBort.Text = "Ta bort";
            btnTaBort.UseVisualStyleBackColor = true;
            btnTaBort.Click += btnTaBort_Click;
            // 
            // textURL
            // 
            textURL.Location = new Point(500, 92);
            textURL.Name = "textURL";
            textURL.Size = new Size(192, 23);
            textURL.TabIndex = 4;
            // 
            // comboBoxFiltrera
            // 
            comboBoxFiltrera.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFiltrera.FormattingEnabled = true;
            comboBoxFiltrera.Location = new Point(468, 169);
            comboBoxFiltrera.Name = "comboBoxFiltrera";
            comboBoxFiltrera.Size = new Size(140, 23);
            comboBoxFiltrera.TabIndex = 5;
            comboBoxFiltrera.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // labelURL
            // 
            labelURL.AutoSize = true;
            labelURL.Location = new Point(463, 95);
            labelURL.Name = "labelURL";
            labelURL.Size = new Size(31, 15);
            labelURL.TabIndex = 6;
            labelURL.Text = "URL:";
            // 
            // btnAterstall
            // 
            btnAterstall.Location = new Point(614, 169);
            btnAterstall.Name = "btnAterstall";
            btnAterstall.Size = new Size(67, 21);
            btnAterstall.TabIndex = 7;
            btnAterstall.Text = "Återställ";
            btnAterstall.UseVisualStyleBackColor = true;
            btnAterstall.Click += btnAterstall_Click;
            // 
            // textNamn
            // 
            textNamn.Location = new Point(200, 92);
            textNamn.Name = "textNamn";
            textNamn.Size = new Size(192, 23);
            textNamn.TabIndex = 8;
            // 
            // cbxKategori
            // 
            cbxKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxKategori.FormattingEnabled = true;
            cbxKategori.Location = new Point(200, 129);
            cbxKategori.Name = "cbxKategori";
            cbxKategori.Size = new Size(190, 23);
            cbxKategori.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(864, 196);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 11;
            label1.Text = "Avsnitt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1123, 161);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 12;
            label2.Text = "Kategorier";
            // 
            // DeleteKategori
            // 
            DeleteKategori.Location = new Point(1271, 305);
            DeleteKategori.Name = "DeleteKategori";
            DeleteKategori.Size = new Size(67, 21);
            DeleteKategori.TabIndex = 15;
            DeleteKategori.Text = "Ta bort";
            DeleteKategori.UseVisualStyleBackColor = true;
            DeleteKategori.Click += DeleteKategori_Click;
            // 
            // EditKategori
            // 
            EditKategori.Location = new Point(1271, 266);
            EditKategori.Name = "EditKategori";
            EditKategori.Size = new Size(67, 21);
            EditKategori.TabIndex = 14;
            EditKategori.Text = "Ändra";
            EditKategori.UseVisualStyleBackColor = true;
            EditKategori.Click += EditKategori_Click;
            // 
            // AddKategori
            // 
            AddKategori.Location = new Point(1271, 226);
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
            listBoxKategori.Location = new Point(1052, 226);
            listBoxKategori.Name = "listBoxKategori";
            listBoxKategori.Size = new Size(213, 169);
            listBoxKategori.TabIndex = 16;
            listBoxKategori.SelectedIndexChanged += listBoxKategori_SelectedIndexChanged;
            // 
            // richTextBeskrivning
            // 
            richTextBeskrivning.Location = new Point(747, 536);
            richTextBeskrivning.Name = "richTextBeskrivning";
            richTextBeskrivning.Size = new Size(273, 141);
            richTextBeskrivning.TabIndex = 17;
            richTextBeskrivning.Text = "";
            // 
            // listBoxAvsnitt
            // 
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.ItemHeight = 15;
            listBoxAvsnitt.Location = new Point(747, 226);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(273, 274);
            listBoxAvsnitt.TabIndex = 18;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            // 
            // listPodd
            // 
            listPodd.Columns.AddRange(new ColumnHeader[] { namn, antalAvsnitt, titel, kategori, Uppdateringsintervall });
            listPodd.FullRowSelect = true;
            listPodd.Location = new Point(158, 226);
            listPodd.Name = "listPodd";
            listPodd.Size = new Size(553, 451);
            listPodd.TabIndex = 19;
            listPodd.UseCompatibleStateImageBehavior = false;
            listPodd.View = View.Details;
            listPodd.SelectedIndexChanged += listPodd_SelectedIndexChanged;
            // 
            // namn
            // 
            namn.Text = "Namn";
            namn.Width = 80;
            // 
            // antalAvsnitt
            // 
            antalAvsnitt.Text = "Antal Avsnitt";
            antalAvsnitt.Width = 85;
            // 
            // titel
            // 
            titel.Text = "Titel";
            titel.Width = 150;
            // 
            // kategori
            // 
            kategori.Text = "Kategori";
            kategori.Width = 110;
            // 
            // Uppdateringsintervall
            // 
            Uppdateringsintervall.Text = "Uppdateringsintervall";
            Uppdateringsintervall.Width = 130;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(119, 100);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 20;
            label3.Text = "Ange Namn:";
            // 
            // textBoxKategori
            // 
            textBoxKategori.Location = new Point(1123, 188);
            textBoxKategori.Name = "textBoxKategori";
            textBoxKategori.Size = new Size(136, 23);
            textBoxKategori.TabIndex = 21;
            // 
            // labelKategori
            // 
            labelKategori.AutoSize = true;
            labelKategori.Location = new Point(1046, 193);
            labelKategori.Name = "labelKategori";
            labelKategori.Size = new Size(71, 15);
            labelKategori.TabIndex = 22;
            labelKategori.Text = "Ny kategori:";
            // 
            // labelPodd
            // 
            labelPodd.AutoSize = true;
            labelPodd.Location = new Point(395, 45);
            labelPodd.Name = "labelPodd";
            labelPodd.Size = new Size(49, 15);
            labelPodd.TabIndex = 23;
            labelPodd.Text = "Podcast";
            // 
            // labelBeskrivning
            // 
            labelBeskrivning.AutoSize = true;
            labelBeskrivning.Location = new Point(831, 518);
            labelBeskrivning.Name = "labelBeskrivning";
            labelBeskrivning.Size = new Size(110, 15);
            labelBeskrivning.TabIndex = 26;
            labelBeskrivning.Text = "Avsnittsbeskrivning";
            // 
            // comboBoxIntervall
            // 
            comboBoxIntervall.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxIntervall.FormattingEnabled = true;
            comboBoxIntervall.Location = new Point(200, 161);
            comboBoxIntervall.Name = "comboBoxIntervall";
            comboBoxIntervall.Size = new Size(190, 23);
            comboBoxIntervall.TabIndex = 27;
            // 
            // labelIntervall
            // 
            labelIntervall.AutoSize = true;
            labelIntervall.Location = new Point(51, 164);
            labelIntervall.Name = "labelIntervall";
            labelIntervall.Size = new Size(143, 15);
            labelIntervall.TabIndex = 28;
            labelIntervall.Text = "Välj uppdateringsintervall:";
            // 
            // labelUppdatering
            // 
            labelUppdatering.AutoSize = true;
            labelUppdatering.Location = new Point(414, 680);
            labelUppdatering.Name = "labelUppdatering";
            labelUppdatering.Size = new Size(83, 15);
            labelUppdatering.TabIndex = 29;
            labelUppdatering.Text = "Uppdateringar";
            // 
            // helpButton
            // 
            helpButton.Location = new Point(396, 161);
            helpButton.Name = "helpButton";
            helpButton.Size = new Size(30, 23);
            helpButton.TabIndex = 30;
            helpButton.Text = "?";
            helpButton.UseVisualStyleBackColor = true;
            helpButton.Click += helpButton_Click;
            // 
            // listBoxUpd
            // 
            listBoxUpd.FormattingEnabled = true;
            listBoxUpd.ItemHeight = 15;
            listBoxUpd.Location = new Point(158, 713);
            listBoxUpd.Name = "listBoxUpd";
            listBoxUpd.Size = new Size(553, 64);
            listBoxUpd.TabIndex = 31;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1376, 796);
            Controls.Add(listBoxUpd);
            Controls.Add(helpButton);
            Controls.Add(labelUppdatering);
            Controls.Add(labelIntervall);
            Controls.Add(comboBoxIntervall);
            Controls.Add(labelBeskrivning);
            Controls.Add(labelPodd);
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
            Controls.Add(labelValjKategori);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Podcast";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelValjKategori;
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
        private Label labelPodd;
        private Label labelBeskrivning;
        private ComboBox comboBoxIntervall;
        private Label labelIntervall;
        private ColumnHeader Uppdateringsintervall;
        private Label labelUppdatering;
        private Button helpButton;
        private Label labelTidTitel;
        private Label labelNuvarandeTid;
        private ListBox listBoxUpd;
    }
}
