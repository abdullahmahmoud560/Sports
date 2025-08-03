using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class GoalsReports
    {
        public int Id { get; set; } 
        public string PlayerName { get; set; } = string.Empty;
        public string AcadamyName { get; set; } = string.Empty; 
        public string Minute { get; set; } = string.Empty; 
        public string? Notes { get; set; }
        public Match? Match { get; set; }
        [ForeignKey(nameof(Match))]
        public int MatchId { get; set; }
    }

}
