namespace GhibliAPI.Model
{
    public class Movie
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Award { get; set; } = string.Empty;
    }
}
