using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class PlayersReport
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;  // بدل Possition
        public string Essential { get; set; } = string.Empty;
        public string? Reserve { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public Match? Match { get; set; }
        [ForeignKey(nameof(Match))]
        public int MatchId { get; set; }

    }
}
