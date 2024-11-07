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

        //samlar alla metoder som ska köras vid start för att lättare kunna justera dem
        private void startaForm()
        {
            hamtaAllaPoddar();
            hamtaAllaKategorier();
            richTextBeskrivning.ReadOnly = true;
            UppdateraPodcastsVidStart();

            //logik för timers
            poddKontroll.PodcastUpdated += OnUppdateringMeddelande;
            poddKontroll.StartaTimersPaBefintligaPoddar();
            resetFalt();
        }

        //metod för att visa meddelande då podcast uppdateas
        private void OnUppdateringMeddelande(string message)
        {
            if (InvokeRequired)
            {
                //måste göra en egen "invoke" eftersom det är flera trådar som arbetar samtidigt
                BeginInvoke(new Action<string>(OnUppdateringMeddelande), message);
            }
            else
            {
                //tömma uppdateringsboxen 
                if(listBoxUpd.Items.Count > listPodd.Items.Count)
                {
                    listBoxUpd.Items.Clear();
                }
                
                listBoxUpd.Items.Add(message);
            }

            //för att avsnitten ska uppdateras direkt i avsnittboxen, så man slipper klicka på en podcast igen för att visa nya avsnitt
            if (listPodd.SelectedItems.Count > 0)
            {
                var selectedPodcast = poddKontroll.getPoddar()[listPodd.SelectedIndices[0]];
                uppdateringAvsnittListaVidHamtning(selectedPodcast);
            }
        }
        private void uppdateringAvsnittListaVidHamtning(Podcast selectedPodcast)
        {
            listBoxAvsnitt.BeginUpdate();
            listBoxAvsnitt.Items.Clear();

            foreach (Avsnitt episode in selectedPodcast.poddAvsnitt)
            {
                listBoxAvsnitt.Items.Add(episode);
            }

            listBoxAvsnitt.EndUpdate();
        }


        //metod för att hämta nya avsnitt för alla sparade poddar vid start
        private async void UppdateraPodcastsVidStart()
        {
            bool flagga = false;
            foreach (Podcast p in poddKontroll.getPoddar())
            {
                try
                {
                    await poddKontroll.FetchBaraAvsnitt(p);
                    flagga = true;
                    
                }
                catch (Exception ex)
                {
                    validering.visaFelmeddelande("Fel vid startuppdatering av podcast "+p.Titel, ex);
                }
            }

            if (flagga)
            {
                listBoxUpd.Items.Add("Alla poddar uppdaterades vid start "+DateTime.Now);
            }
        }

        //hårdkodade intervallval
        private void laddaCbxIntervall()
        {
            comboBoxIntervall.Items.Add("1 minut");
            comboBoxIntervall.Items.Add("5 minuter");
            comboBoxIntervall.Items.Add("10 minuter");
            comboBoxIntervall.Items.Add("30 minuter");
            comboBoxIntervall.Items.Add("60 minuter");

        }

        //hämtar alla sparade poddar och fyller listan
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

        //hämtar alla kategorier och fyller listan
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


        //knapp för att lägga till nytt flöde
        private async void btnLaggTill_Click(object sender, EventArgs e)
        {
            //hämtar alla ifyllda värden i UI
            string url = textURL.Text;
            string egetNamn = textNamn.Text;
            string? kategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : "-";

            //standardintervall ifall inget är valt (error ifall man la 0)
            int valdIntervall = 120;
            if (validering.valideringNamn(comboBoxIntervall.Text))
            {
                string[] intervall = comboBoxIntervall.Text.Split(' ');
                valdIntervall = Int32.Parse(intervall[0]);
            }

            try
            {
                //validering, testar hämta flödet för att se att det är ett rss flöde
                if (validering.poddFinnsInte(url))
                {
                    if (validering.valideringXml(url))
                    {
                        //hämta podden
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

            //återställer fälten
            resetFalt();

        }

        //när man väljer ett objekt i podcast listan
        private void listPodd_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAvsnitt.BeginUpdate(); 
            listBoxAvsnitt.Items.Clear();
            richTextBeskrivning.Clear();

            if (listPodd.SelectedItems.Count > 0)
            {
                //fyller avsnittslistan med avsnitt som hör till den valda podden, kollar på index
                var selectedPodcast = poddKontroll.getPoddar()[listPodd.SelectedIndices[0]];

                foreach (Avsnitt episode in selectedPodcast.poddAvsnitt)
                {
                    listBoxAvsnitt.Items.Add(episode);
                }
            }

            listBoxAvsnitt.EndUpdate();
        }

        //när man väljer ett avsnitt
        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBeskrivning.Clear();

            if (listBoxAvsnitt.SelectedItem != null)
            {
                //visar datum för avsnittet och beskrivningen
                Avsnitt selectedEpisode = (Avsnitt)listBoxAvsnitt.SelectedItem;
                richTextBeskrivning.Text = "Datum: "+selectedEpisode.PublishDate + "\n"+"Beskrivning: "+selectedEpisode.Description;
            }
        }


        //knapp för att ta bort podcast
        private void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listPodd.SelectedItems.Count > 0)
            {
                try
                {
                    int valdPodd = listPodd.SelectedIndices[0];

                    //bekräftelse på borttagning
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


        //logik för att ändra värden på en podcast
        //tre stycken bool metoder som validerar och kör metoder för kategori, intervall och namn
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


        private void btnAndra_Click_1(object sender, EventArgs e)
        {
            //kör de tre metoderna
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

        //metod för att uppdatera listan med poddar
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

        //två metoder för att att uppdatera kategorier
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


        //borttagning av kategori
        private void DeleteKategori_Click(object sender, EventArgs e)
        {
            int valdKategori = listBoxKategori.SelectedIndex;
            try
            {
                if (valdKategori != -1)
                {
                    //bekräfta val
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

        //lägga till kategori
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

        //redigera kategori
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
    }
}
