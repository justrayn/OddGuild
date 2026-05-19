namespace OddGuild.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string? Title { get; set; }        
        public string? Type { get; set; }         
        public string? Description { get; set; }  
        public int Bounty { get; set; }
        public string? Location { get; set; }     
        public bool IsMine { get; set; } // Keeps track of which tab to display the quest in
    }
}