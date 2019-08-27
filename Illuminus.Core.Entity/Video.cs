namespace Illuminus.Core.Entity
{
    public class Video
    {
        public Video(int id, string name, string genre)
        {
            Id = id;
            Name = name;
            Genre = genre;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
    }
}
