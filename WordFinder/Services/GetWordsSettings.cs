namespace WordFinder.Services
{
    public class GetWordsSettings
    {
        public int MinimumCharacters { get; }

        public GetWordsSettings(int minimumCharacters)
        {
            this.MinimumCharacters = minimumCharacters;
        }
    }
}
