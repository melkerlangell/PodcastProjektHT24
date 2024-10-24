using Modeller;
using BusinessLayer;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
            startForm();
        }

        private void startForm()
        {
            hamtaAllaPoddar();
            hamtaAllaKategorier();
            resetFalt();
            richTextBeskrivning.ReadOnly = true;
        }

        private void hamtaAllaPoddar()
        {
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

                listPodd.Items.Add(podcastItem);
            }
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


        private void btnLaggTill_Click(object sender, EventArgs e)
        {
            string url = textURL.Text;
            string egetNamn = textNamn.Text;
            string? kategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : "-";

            try
            {
                if (validering.valideringXml(url))
                {
                    poddKontroll.FetchRssPoddar(url, egetNamn, kategori);
                    listPodd.Items.Clear();
                    hamtaAllaPoddar();
                }
                else
                {
                    MessageBox.Show("Ange ett giltigt rss fl�de");
                }
                

            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid h�mtning av poddar", ex);
            }

            resetFalt();

        }

        private void listPodd_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        }

        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBeskrivning.Clear();

            if (listBoxAvsnitt.SelectedItem != null)
            {
                Avsnitt selectedEpisode = (Avsnitt)listBoxAvsnitt.SelectedItem;
                richTextBeskrivning.Text = selectedEpisode.Description;
            }
        }



        private void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listPodd.SelectedItems.Count > 0)
            {
                try
                {
                    int valdPodd = listPodd.SelectedIndices[0];
                    var bekraftaVal = MessageBox.Show("�r du s�ker p� att du vill ta bort fl�det?", "Bekr�fta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                MessageBox.Show("V�lj vilket fl�de du vill ta bort");
            }
            resetFalt();
        }

        private Boolean andraKategoriForPodd()
        {

            string? nyKategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : null;  
            bool flagga = false;

            try
            {
                if (validering.valideringNamn(nyKategori))
                {
                    int valdPoddIndex = listPodd.SelectedIndices[0]; 

                    poddKontroll.AndraPoddKategori(valdPoddIndex, nyKategori);

                    flagga = true;
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid �ndring av kategori", ex);
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
                validering.visaFelmeddelande("Fel vid �ndring av namn", ex);
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


        private void btnAndra_Click_1(object sender, EventArgs e)
        {
            bool namnAndrat = andraNamnPodcast();
            bool kategoriAndrad = andraKategoriForPodd();

         
            if (namnAndrat || kategoriAndrad)
            {
                uppdateraPoddLista();
                resetFalt();
            }
            else
            {
                MessageBox.Show("V�lj en podcast i listan och �ndra egenskaper innan du sparar");
                               
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
                    DialogResult dialogResult = MessageBox.Show("�r du s�ker p� att du vill ta bort denna kategori?", "Bekr�fta borttagning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    MessageBox.Show("V�nligen v�lj en kategori att ta bort.", "Ingen kategori vald", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Ange ett nytt namn i textf�ltet");
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid till�ggning av kategori", ex);
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
                    MessageBox.Show("V�lj kategorin du vill �ndra namn p� och ange det nya namnet i textf�ltet");
                }
            }catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid redigering av kategori", ex);
            }
        }

        private void FiltreraKategori()
        {
            string valdKategori = comboBoxFiltrera.SelectedItem?.ToString();

            if (valdKategori != null)
            {
                listPodd.Items.Clear();
                List<Podcast> poddar = poddKontroll.getPoddar();

                foreach (Podcast p in poddar)
                {
                    if (p.Kategori == valdKategori)
                    {
                        ListViewItem podcastItem = new ListViewItem(p.EgetNamn);
                        podcastItem.SubItems.Add(p.AntalAvsnitt.ToString());
                        podcastItem.SubItems.Add(p.Titel);
                        podcastItem.SubItems.Add(p.Kategori ?? "Ingen kategori");

                        listPodd.Items.Add(podcastItem);
                    }
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
            string text1 = "V�lj kategori";
            cbxKategori.Text = text1;
            textURL.Text = "";
        }

        
    }
}
