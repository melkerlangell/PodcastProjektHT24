using DAL;
using BLL;


namespace PodcastProjektHT24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
        }

        private async void btnUrl_Click(object sender, EventArgs e)
        {
            try
            {
                HamtaPodcast rssHanterare = new HamtaPodcast();

                string url = textBoxUrl.Text;

                List<PoddAvsnitt> podd = await rssHanterare.GetRss(url);

                foreach (PoddAvsnitt poddItem in podd)
                {
                    listBoxAvsnitt.Items.Add(poddItem);
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAvsnitt.SelectedItem is PoddAvsnitt selectedEpisode)
            {
                richTextBoxDesc.Clear();
                
                richTextBoxDesc.Text = selectedEpisode.Beskrivning;
                
            }
        }
    }
}
