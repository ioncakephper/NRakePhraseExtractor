namespace WindowsFormsApp5
{
    public class Topic
    {
        private string _filepath;

        public Topic()
        {
        }

        public Topic(string filePath) : this()
        {
            Filepath = filePath;
            Title = GetTitle();
        }

        public virtual string GetTitle()
        {
            return System.IO.Path.GetFileNameWithoutExtension(Filepath);
        }

        public string Title { get; set; }
        public string Filename { get; internal set; }
        public string Folder { get; internal set; }
        public string Filepath
        {
            get { return _filepath; }
            set
            {
                _filepath = value;
                Filename = System.IO.Path.GetFileName(_filepath);
                Folder = System.IO.Path.GetDirectoryName(_filepath);
            }
        }
    }
}