namespace OddGuild.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string? Title { get; set; }        // Added ?
        public string? Type { get; set; }         // Added ?
        public string? Description { get; set; }  // Added ?
        public int Bounty { get; set; }
        public string? Location { get; set; }     // Added ?
    }
}