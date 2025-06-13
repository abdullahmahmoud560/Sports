using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Sports.Model
{
    public class Match
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public DateTime? Date { get; set; }
        public TimeOnly? Time { get; set; }
        public string? Stadium { get; set; }
        public string? MatchStatus { get; set; }
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public int HomeTeamScore { get; set; } = 0;
        public int AwayTeamScore { get; set; } = 0;
    }
}
