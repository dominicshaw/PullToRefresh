namespace PullToRefresh
{
    public class Tweet
    {
        public string Author { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Author}: {Text}";
        }
    }
}
