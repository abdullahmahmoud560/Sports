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
        public TimeOnly? Time { get; set; }
        public string? Stadium { get; set; }
        public string? MatchStatus { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
