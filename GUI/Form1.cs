using Modeller;
using BusinessLayer;


namespace GUI
{
    public partial class Form1 : Form
    {
        private PodcastController poddKontroll;
        private KategoriController kategoriController;




        public Form1()
        {
            InitializeComponent();
            poddKontroll = new PodcastController();
            hamtaAllaPoddar();
            kategoriController = new KategoriController();
            displayKat();
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

       
        private void displayKat()
        {
            List<Kategori> kategorier = kategoriController.DisplayKategorier();
            if (kategorier == null)
                { return; }

            foreach (Kategori k in kategorier)
            {

            }
        }


        private void btnLaggTill_Click(object sender, EventArgs e)
        {
            string url = textURL.Text;
            string egetNamn = textNamn.Text;

            try
            {
                poddKontroll.FetchRssPoddar(url, egetNamn);

                listPodd.Items.Clear();
                hamtaAllaPoddar();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error vid hämtning av poddar {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textURL.Clear();

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

        private void btnAndra_Click(object sender, EventArgs e)
        {

        }

        private void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listPodd.SelectedItems.Count > 0)
            {
                try
                {
                    int valdPodd = listPodd.SelectedIndices[0];
                    var bekraftaVal = MessageBox.Show("Är du säker på att du vill ta bort flödet?", "Bekräfta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        }
    }
}
