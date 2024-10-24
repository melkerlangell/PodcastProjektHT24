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

        public Form1()
        {
            InitializeComponent();
            poddKontroll = new PodcastController();
            katKontroll = new KategoriController();

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
                podcastItem.SubItems.Add(p.Kategori ?? "Ingen kategori");

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
                poddKontroll.FetchRssPoddar(url, egetNamn, kategori);

                listPodd.Items.Clear();
                hamtaAllaPoddar();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error vid hämtning av poddar {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    var bekraftaVal = MessageBox.Show("Är du säker på att du vill ta bort flödet?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (bekraftaVal == DialogResult.Yes)
                    {
                        poddKontroll.TaBortPodd(valdPodd);
                        listPodd.Items.Clear();
                        hamtaAllaPoddar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error vid borttagning av podd: {ex.Message}");
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

            string? nyKategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : null;  // Ny kategori för podcasten
            bool flagga = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(nyKategori))
                {
                    int valdPoddIndex = listPodd.SelectedIndices[0];  // Hämta indexet för den valda podcasten
                    poddKontroll.AndraPoddKategori(valdPoddIndex, nyKategori);

                    flagga = true;
                }
            }
            catch (Exception ex)
            {

            }
            return flagga;
        }


        private Boolean andraNamnPodcast()
        {
            string nyttNamn = textNamn.Text;
            bool flagga = false;

            try
            {
                if (listPodd.SelectedItems.Count > 0 && !string.IsNullOrWhiteSpace(nyttNamn))
                {
                    int valdPoddIndex = listPodd.SelectedIndices[0];  // Hämta indexet för den valda podcasten
                    // Om ett nytt namn har angetts, uppdatera namnet
                    if (!string.IsNullOrWhiteSpace(nyttNamn))
                    {
                        poddKontroll.AndraPoddNamn(valdPoddIndex, nyttNamn);
                        flagga = true;
                    }
                }
            }
            catch (Exception ex)
            {

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
                MessageBox.Show("Välj en podcast i listan och ändra egenskaper innan du sparar",
                                "Fel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string nyttNamn = "";
            if (valdKategori != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Är du säker på att du vill ta bort denna kategori?", "Bekräfta borttagning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    katKontroll.TaBortKategori(valdKategori);
                    poddKontroll.AndraPoddKategori(valdKategori, nyttNamn);
                    uppdateraListaOchCbx(valdKategori);
                    listPodd.Items.Clear();
                    hamtaAllaPoddar();
                }
            }
            else
            {
                MessageBox.Show("Vänligen välj en kategori att ta bort.", "Ingen kategori vald", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddKategori_Click(object sender, EventArgs e)
        {
            string nyKategori = textBoxKategori.Text;


            if (!string.IsNullOrWhiteSpace(nyKategori))
            {
                katKontroll.LaggTillKat(nyKategori);
                uppdateraListaOchCbx(nyKategori);
                textBoxKategori.Text = "";
            }
        }

        private void EditKategori_Click(object sender, EventArgs e)
        {
            string nyttNamn = textBoxKategori.Text;
            int valdKategori = listBoxKategori.SelectedIndex;

            if (!string.IsNullOrWhiteSpace(nyttNamn))
            {
                katKontroll.AndraKategoriNamn(valdKategori, nyttNamn);
                poddKontroll.AndraPoddKategori(valdKategori, nyttNamn);
                listBoxKategori.Items.Clear();
                cbxKategori.Items.Clear();
                comboBoxFiltrera.Items.Clear();
                hamtaAllaKategorier();
                listPodd.Items.Clear();
                hamtaAllaPoddar();
                
            }
        }

        private void FiltreraKategori()
        {
            string valdKategori = comboBoxFiltrera.SelectedItem?.ToString();

            if (valdKategori is not null)
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
            string text = "filtrera...";
            comboBoxFiltrera.Text = text;
            textNamn.Text = "";
            string text1 = "Välj kategori";
            cbxKategori.Text = text1;
            textURL.Text = "";
        }

        
    }
}
