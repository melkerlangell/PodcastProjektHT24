using System;
using Modeller;
using BusinessLayer;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.CompilerServices;



namespace GUI
{
    public partial class Form1 : Form
    {
        private PodcastController poddKontroll;
        private KategoriController katKontroll;
        private Validering validering;


        public Form1()
        {
            InitializeComponent();
            poddKontroll = new PodcastController();
            katKontroll = new KategoriController();
            validering = new Validering();
            startaForm();
            
        }

        private void startaForm()
        {
            hamtaAllaPoddar();
            hamtaAllaKategorier();
            resetFalt();
            richTextBeskrivning.ReadOnly = true;
            UppdateringPoddar();
            UppdateraPodcastsVidStart();
        }

        private async void UppdateraPodcastsVidStart()
        {
            foreach (Podcast p in poddKontroll.getPoddar())
            {
                try
                {
                    await poddKontroll.FetchBaraAvsnitt(p);
                    uppdateraPoddLista(); 
                }
                catch (Exception ex)
                {
                    validering.visaFelmeddelande("Fel vid startuppdatering av podcast "+p.Titel, ex);
                }
            }
        }



        private async void UppdateringPoddar()
        {
            foreach (Podcast p in poddKontroll.getPoddar())
            {
                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                t.Interval = p.uppdateringsIntervall * 60000;

                t.Tick += async (sender, args) =>
                {

                    try
                    {
                        await poddKontroll.FetchBaraAvsnitt(p);
                        p.AntalAvsnitt = p.poddAvsnitt.Count;
                        labelUppdatering.Text = "Podcast: " + p.Titel + " uppdaterades " + DateTime.Now;
                        uppdateringPoddUtanLista();
                    }
                    catch (Exception ex)
                    {
                        validering.visaFelmeddelande("Fel vid uppdatering av podcast " + p.Titel, ex);
                    }
                };
                t.Start();
            }
        }



        private void laddaCbxIntervall()
        {
            comboBoxIntervall.Items.Add("1 minut");
            comboBoxIntervall.Items.Add("5 minuter");
            comboBoxIntervall.Items.Add("10 minuter");
            comboBoxIntervall.Items.Add("30 minuter");
            comboBoxIntervall.Items.Add("60 minuter");

        }
        private void hamtaAllaPoddar()
        {
            listPodd.BeginUpdate();
            List<Podcast> poddar = poddKontroll.getPoddar();
            if (poddar == null)
            {
                return;
            }

            foreach (Podcast p in poddar)
            {
                ListViewItem podcastItem = new ListViewItem(p.EgetNamn);
                podcastItem.SubItems.Add(p.AntalAvsnitt.ToString());
                podcastItem.SubItems.Add(p.Titel);
                podcastItem.SubItems.Add(p.Kategori ?? "-");
                podcastItem.SubItems.Add(p.uppdateringsIntervall + " minuter" ?? "-");

                listPodd.Items.Add(podcastItem);
            }
            listPodd.EndUpdate();
        }

        private void hamtaAllaKategorier()
        {
            List<Kategori> kategorier = katKontroll.getKategorier();
            if (kategorier == null)
            {
                return;
            }

            foreach (Kategori k in kategorier)
            {
                listBoxKategori.Items.Add(k.Namn);
                cbxKategori.Items.Add(k.Namn);
                comboBoxFiltrera.Items.Add(k.Namn);
            }
        }


        private async void btnLaggTill_Click(object sender, EventArgs e)
        {
            string url = textURL.Text;
            string egetNamn = textNamn.Text;
            string? kategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : "-";

            int valdIntervall = 120;
            if (validering.valideringNamn(comboBoxIntervall.Text))
            {
                string[] intervall = comboBoxIntervall.Text.Split(' ');
                valdIntervall = Int32.Parse(intervall[0]);
            }

            try
            {
                if (validering.poddFinnsInte(url))
                {
                    if (validering.valideringXml(url))
                    {
                        await poddKontroll.FetchRssPoddar(url, egetNamn, kategori, valdIntervall);
                        uppdateraPoddLista();
                    }
                    else
                    {

                        MessageBox.Show("Ange ett giltigt rss flöde");
                    }

                }
                else
                {
                    MessageBox.Show("Flödet existerar redan, onödigt med dubbletter ;)");
                }


            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid hämtning av poddar", ex);
            }

            resetFalt();

        }

        private void listPodd_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAvsnitt.BeginUpdate(); 
            listBoxAvsnitt.Items.Clear();
            richTextBeskrivning.Clear();

            if (listPodd.SelectedItems.Count > 0)
            {
                var selectedPodcast = poddKontroll.getPoddar()[listPodd.SelectedIndices[0]];

                foreach (Avsnitt episode in selectedPodcast.poddAvsnitt)
                {
                    listBoxAvsnitt.Items.Add(episode);
                }
            }

            listBoxAvsnitt.EndUpdate();
        }

        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBeskrivning.Clear();

            if (listBoxAvsnitt.SelectedItem != null)
            {
                Avsnitt selectedEpisode = (Avsnitt)listBoxAvsnitt.SelectedItem;
                richTextBeskrivning.Text = "Datum: "+selectedEpisode.PublishDate + "\n"+"Beskrivning: "+selectedEpisode.Description;
            }
        }



        private void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listPodd.SelectedItems.Count > 0)
            {
                try
                {
                    int valdPodd = listPodd.SelectedIndices[0];
                    var bekraftaVal = MessageBox.Show("Är du säker på att du vill ta bort flödet?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (bekraftaVal == DialogResult.Yes)
                    {
                        poddKontroll.TaBortPodd(valdPodd);
                        uppdateraPoddLista();
                    }
                }
                catch (Exception ex)
                {
                    validering.visaFelmeddelande("Fel vid borttagning av podd", ex);
                }
            }
            else
            {
                MessageBox.Show("Välj vilket flöde du vill ta bort");
            }
            resetFalt();
        }

        private Boolean andraKategoriForPodd()
        {

            string? nyKategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : null;
            bool flagga = false;

            try
            {
                if (listPodd.SelectedItems.Count > 0 && validering.valideringNamn(nyKategori))
                {
                    int valdPoddIndex = listPodd.SelectedIndices[0];

                    poddKontroll.AndraPoddKategori(valdPoddIndex, nyKategori);

                    flagga = true;
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid ändring av kategori", ex);
            }
            return flagga;
        }

        private Boolean andraIntervallPodcast()
        {
            string? nyttIntervall = comboBoxIntervall.SelectedItem != null ? comboBoxIntervall.SelectedItem.ToString() : null;
            bool flagga = false;

            try
            {
                if (listPodd.SelectedItems.Count > 0 && validering.valideringNamn(nyttIntervall))
                {
                    string[] intervall = nyttIntervall.Split(" ");

                    string valdIntervall = intervall[0];

                    int valdPoddIndex = listPodd.SelectedIndices[0];

                    poddKontroll.AndraPoddIntervall(valdPoddIndex, valdIntervall);

                    flagga = true;
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid ändring av intervall", ex);
            }
            return flagga;
        }


        private Boolean andraNamnPodcast()
        {
            string nyttNamn = textNamn.Text;
            bool flagga = false;

            try
            {
                if (listPodd.SelectedItems.Count > 0 && validering.valideringNamn(nyttNamn))
                {
                    int valdPoddIndex = listPodd.SelectedIndices[0];

                    poddKontroll.AndraPoddNamn(valdPoddIndex, nyttNamn);
                    flagga = true;

                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid ändring av namn", ex);
            }
            return flagga;
        }

        private void uppdateraPoddLista()
        {
            listPodd.Items.Clear();
            hamtaAllaPoddar();
            cbxKategori.Items.Clear();
            listBoxKategori.Items.Clear();
            comboBoxFiltrera.Items.Clear();
            hamtaAllaKategorier();
            listBoxAvsnitt.Items.Clear();
        }

        private void uppdateringPoddUtanLista()
        {
            listPodd.Items.Clear();
            hamtaAllaPoddar();
        }


        private void btnAndra_Click_1(object sender, EventArgs e)
        {
            bool namnAndrat = andraNamnPodcast();
            bool kategoriAndrad = andraKategoriForPodd();
            bool intervallAndrad = andraIntervallPodcast();


            if (namnAndrat || kategoriAndrad || intervallAndrad)
            {
                uppdateraPoddLista();
                resetFalt();
            }
            else
            {
                MessageBox.Show("Välj en podcast i listan och ändra någon egenskap innan du sparar");

            }

        }

        

        private void uppdateraListaOchCbx(string kategori)
        {
            cbxKategori.Items.Add(kategori);
            listBoxKategori.Items.Add(kategori);
            comboBoxFiltrera.Items.Add(kategori);
        }

        private void uppdateraListaOchCbx(int index)
        {
            cbxKategori.Items.RemoveAt(index);
            listBoxKategori.Items.RemoveAt(index);
            comboBoxFiltrera.Items.RemoveAt(index);
        }



        private void DeleteKategori_Click(object sender, EventArgs e)
        {
            int valdKategori = listBoxKategori.SelectedIndex;
            try
            {
                if (valdKategori != -1)
                {
                    DialogResult dialogResult = MessageBox.Show("Är du säker på att du vill ta bort denna kategori?", "Bekräfta borttagning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string vald = listBoxKategori.SelectedItem.ToString();

                        katKontroll.TaBortKategori(valdKategori);

                        poddKontroll.UppdateraPodcastsKategori(vald, "");

                        uppdateraPoddLista();
                    }
                }
                else
                {
                    MessageBox.Show("Vänligen välj en kategori att ta bort.", "Ingen kategori vald", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid borttagning av kategori", ex);
            }
        }

        private void AddKategori_Click(object sender, EventArgs e)
        {
            string nyKategori = textBoxKategori.Text;

            try
            {
                if (validering.valideringNamn(nyKategori))
                {
                    katKontroll.LaggTillKat(nyKategori);
                    uppdateraListaOchCbx(nyKategori);
                    textBoxKategori.Text = "";
                }
                else
                {
                    MessageBox.Show("Ange ett nytt namn i textfältet");
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid tilläggning av kategori", ex);
            }
        }

        private void EditKategori_Click(object sender, EventArgs e)
        {
            string nyttNamn = textBoxKategori.Text;
            int valdKategori = listBoxKategori.SelectedIndex;

            try
            {
                if (validering.valideringNamn(nyttNamn) && valdKategori != -1)
                {
                    string vald = listBoxKategori.SelectedItem.ToString();
                    katKontroll.AndraKategoriNamn(valdKategori, nyttNamn);
                    poddKontroll.UppdateraPodcastsKategori(vald, nyttNamn);

                    uppdateraPoddLista();


                }
                else
                {
                    MessageBox.Show("Välj kategorin du vill ändra namn på och ange det nya namnet i textfältet");
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid redigering av kategori", ex);
            }
        }

        private void FiltreraKategori()
        {
            string valdKategori = comboBoxFiltrera.SelectedItem?.ToString();
            List<Kategori> katt = new List<Kategori>();

            if (valdKategori != null)
            {
                listPodd.Items.Clear();
                var query = poddKontroll.getPoddar().Where(p => p.Kategori.Equals(valdKategori));

                foreach (var p in query)
                {
                    ListViewItem podcastItem = new ListViewItem(p.EgetNamn);
                    podcastItem.SubItems.Add(p.AntalAvsnitt.ToString());
                    podcastItem.SubItems.Add(p.Titel);
                    podcastItem.SubItems.Add(p.Kategori ?? "Ingen kategori");

                    listPodd.Items.Add(podcastItem);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltreraKategori();
        }

        private void listBoxKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAterstall_Click(object sender, EventArgs e)
        {
            resetFalt();

        }


        private void resetFalt()
        {
            listPodd.Items.Clear();
            hamtaAllaPoddar();
            string text = "Filtrera...";
            comboBoxFiltrera.Text = text;
            textNamn.Text = "";
            string text1 = "Välj kategori";
            cbxKategori.Text = text1;
            textURL.Text = "";
            comboBoxIntervall.Items.Clear();
            laddaCbxIntervall();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("När du lägger till en podcast och väljer uppdateringsintervall eller när du ändrar intervallet för en podcast" +
                " måste du starta om applikationen för att den automatiska uppdateringen ska tas i kraft", "Automatisk Uppdatering", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
