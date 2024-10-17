using Modeller;
using DataLayer;
using BusinessLayer;

namespace GUI
{
    public partial class Form1 : Form
    {
        private FetchRss fetcher;
       

        public Form1()
        {
            InitializeComponent();
            fetcher = new FetchRss();
            
           
        }

        

        private void btnLaggTill_Click(object sender, EventArgs e)
        {
            string url = textURL.Text;

            try
            {
                fetcher.FetchPodcast(url);


                listPodd.Items.Clear();


                foreach (Podcast p in fetcher.GetPodcasts())
                {

                    ListViewItem podcastItem = new ListViewItem(p.Titel);
                    podcastItem.SubItems.Add(p.AntalAvsnitt.ToString());
                    podcastItem.SubItems.Add(p.Kategori ?? "Unknown");


                    listPodd.Items.Add(podcastItem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching podcast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listPodd_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxAvsnitt.Items.Clear();
            richTextBeskrivning.Clear();

            if (listPodd.SelectedItems.Count > 0)
            {
                var selectedPodcast = fetcher.GetPodcasts()[listPodd.SelectedIndices[0]];

                foreach (var episode in selectedPodcast.poddAvsnitt)
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
                var selectedEpisode = (Avsnitt)listBoxAvsnitt.SelectedItem;
                richTextBeskrivning.Text = selectedEpisode.Description;
            }
        }

       
    }
}
