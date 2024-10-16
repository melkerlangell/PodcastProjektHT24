namespace PodcastProjektHT24
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
            label1 = new Label();
            labelUrl = new Label();
            textBoxUrl = new TextBox();
            listBoxAvsnitt = new ListBox();
            btnUrl = new Button();
            richTextBoxDesc = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(643, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 21);
            label1.TabIndex = 0;
            label1.Text = "Podcast";
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.Location = new Point(67, 112);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(31, 15);
            labelUrl.TabIndex = 1;
            labelUrl.Text = "URL:";
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(104, 109);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(160, 23);
            textBoxUrl.TabIndex = 2;
            // 
            // listBoxAvsnitt
            // 
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.ItemHeight = 15;
            listBoxAvsnitt.Location = new Point(67, 167);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(264, 184);
            listBoxAvsnitt.TabIndex = 3;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            // 
            // btnUrl
            // 
            btnUrl.Location = new Point(285, 109);
            btnUrl.Name = "btnUrl";
            btnUrl.Size = new Size(123, 23);
            btnUrl.TabIndex = 4;
            btnUrl.Text = "Hämta podcast";
            btnUrl.UseVisualStyleBackColor = true;
            btnUrl.Click += btnUrl_Click;
            // 
            // richTextBoxDesc
            // 
            richTextBoxDesc.Location = new Point(67, 396);
            richTextBoxDesc.Name = "richTextBoxDesc";
            richTextBoxDesc.Size = new Size(264, 178);
            richTextBoxDesc.TabIndex = 5;
            richTextBoxDesc.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1398, 733);
            Controls.Add(richTextBoxDesc);
            Controls.Add(btnUrl);
            Controls.Add(listBoxAvsnitt);
            Controls.Add(textBoxUrl);
            Controls.Add(labelUrl);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Podcast";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label labelUrl;
        private TextBox textBoxUrl;
        private ListBox listBoxAvsnitt;
        private Button btnUrl;
        private RichTextBox richTextBoxDesc;
    }
}
