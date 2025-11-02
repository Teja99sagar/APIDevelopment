namespace MusicApi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string title { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }
    }
}
