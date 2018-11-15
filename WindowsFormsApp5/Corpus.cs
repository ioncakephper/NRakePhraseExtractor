namespace WindowsFormsApp5
{
    public class Corpus
    {
        public Topics Topics
        {
            get; set;
        }

        public Phrases Phrases
        {
            get; set;
        }

        public Corpus()
        {
            Topics = new Topics();
            Phrases = new Phrases();
        }
    }
}