namespace eBooks.ViewModel
{
    public class TreeNodeViewModel
    {
        public string Text { get; set; }
        public string Href { get; set; }
        public List<TreeNodeViewModel> Nodes { get; set; }
    }
}