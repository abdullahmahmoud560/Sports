using Sports.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DTO
{
    public class MatchDTO
    {
        public string? Category { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public string? Stadium { get; set; }
        public string? MatchStatus { get; set; }
        public string? HomeTeamScore { get; set; }
        public string? AwayTeamScore { get; set; }
    }
}
