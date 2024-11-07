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

        //samlar alla metoder som ska k�ras vid start f�r att l�ttare kunna justera dem
        private void startaForm()
        {
            hamtaAllaPoddar();
            hamtaAllaKategorier();
            richTextBeskrivning.ReadOnly = true;
            UppdateraPodcastsVidStart();

            //logik f�r timers
            poddKontroll.PodcastUpdated += OnUppdateringMeddelande;
            poddKontroll.StartaTimersPaBefintligaPoddar();
            resetFalt();
        }

        //metod f�r att visa meddelande d� podcast uppdateas
        private void OnUppdateringMeddelande(string message)
        {
            if (InvokeRequired)
            {
                //m�ste g�ra en egen "invoke" eftersom det �r flera tr�dar som arbetar samtidigt
                BeginInvoke(new Action<string>(OnUppdateringMeddelande), message);
            }
            else
            {
                //t�mma uppdateringsboxen 
                if(listBoxUpd.Items.Count > listPodd.Items.Count)
                {
                    listBoxUpd.Items.Clear();
                }
                
                listBoxUpd.Items.Add(message);
            }

            //f�r att avsnitten ska uppdateras direkt i avsnittboxen, s� man slipper klicka p� en podcast igen f�r att visa nya avsnitt
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


        //metod f�r att h�mta nya avsnitt f�r alla sparade poddar vid start
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

        //h�rdkodade intervallval
        private void laddaCbxIntervall()
        {
            comboBoxIntervall.Items.Add("1 minut");
            comboBoxIntervall.Items.Add("5 minuter");
            comboBoxIntervall.Items.Add("10 minuter");
            comboBoxIntervall.Items.Add("30 minuter");
            comboBoxIntervall.Items.Add("60 minuter");

        }

        //h�mtar alla sparade poddar och fyller listan
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

        //h�mtar alla kategorier och fyller listan
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


        //knapp f�r att l�gga till nytt fl�de
        private async void btnLaggTill_Click(object sender, EventArgs e)
        {
            //h�mtar alla ifyllda v�rden i UI
            string url = textURL.Text;
            string egetNamn = textNamn.Text;
            string? kategori = cbxKategori.SelectedItem != null ? cbxKategori.SelectedItem.ToString() : "-";

            //standardintervall ifall inget �r valt (error ifall man la 0)
            int valdIntervall = 120;
            if (validering.valideringNamn(comboBoxIntervall.Text))
            {
                string[] intervall = comboBoxIntervall.Text.Split(' ');
                valdIntervall = Int32.Parse(intervall[0]);
            }

            try
            {
                //validering, testar h�mta fl�det f�r att se att det �r ett rss fl�de
                if (validering.poddFinnsInte(url))
                {
                    if (validering.valideringXml(url))
                    {
                        //h�mta podden
                        await poddKontroll.FetchRssPoddar(url, egetNamn, kategori, valdIntervall);
                        uppdateraPoddLista();
                    }
                    else
                    {
                        MessageBox.Show("Ange ett giltigt rss fl�de");
                    }
                }
                else
                {
                    MessageBox.Show("Fl�det existerar redan, on�digt med dubbletter ;)");
                }
            }
            catch (Exception ex)
            {
                validering.visaFelmeddelande("Fel vid h�mtning av poddar", ex);
            }

            //�terst�ller f�lten
            resetFalt();

        }

        //n�r man v�ljer ett objekt i podcast listan
        private void listPodd_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAvsnitt.BeginUpdate(); 
            listBoxAvsnitt.Items.Clear();
            richTextBeskrivning.Clear();

            if (listPodd.SelectedItems.Count > 0)
            {
                //fyller avsnittslistan med avsnitt som h�r till den valda podden, kollar p� index
                var selectedPodcast = poddKontroll.getPoddar()[listPodd.SelectedIndices[0]];

                foreach (Avsnitt episode in selectedPodcast.poddAvsnitt)
                {
                    listBoxAvsnitt.Items.Add(episode);
                }
            }

            listBoxAvsnitt.EndUpdate();
        }

        //n�r man v�ljer ett avsnitt
        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBeskrivning.Clear();

            if (listBoxAvsnitt.SelectedItem != null)
            {
                //visar datum f�r avsnittet och beskrivningen
                Avsnitt selectedEpisode = (Avsnitt)listBoxAvsnitt.SelectedItem;
                richTextBeskrivning.Text = "Datum: "+selectedEpisode.PublishDate + "\n"+"Beskrivning: "+selectedEpisode.Description;
            }
        }


        //knapp f�r att ta bort podcast
        private void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listPodd.SelectedItems.Count > 0)
            {
                try
                {
                    int valdPodd = listPodd.SelectedIndices[0];

                    //bekr�ftelse p� borttagning
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


        //logik f�r att �ndra v�rden p� en podcast
        //tre stycken bool metoder som validerar och k�r metoder f�r kategori, intervall och namn
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
                validering.visaFelmeddelande("Fel vid �ndring av kategori", ex);
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
                validering.visaFelmeddelande("Fel vid �ndring av intervall", ex);
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


        private void btnAndra_Click_1(object sender, EventArgs e)
        {
            //k�r de tre metoderna
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
                MessageBox.Show("V�lj en podcast i listan och �ndra n�gon egenskap innan du sparar");

            }

        }

        //metod f�r att uppdatera listan med poddar
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

        //tv� metoder f�r att att uppdatera kategorier
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
                    //bekr�fta val
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

        //l�gga till kategori
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
                    MessageBox.Show("V�lj kategorin du vill �ndra namn p� och ange det nya namnet i textf�ltet");
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
            string text1 = "V�lj kategori";
            cbxKategori.Text = text1;
            textURL.Text = "";
            comboBoxIntervall.Items.Clear();
            laddaCbxIntervall();
        }
    }
}
