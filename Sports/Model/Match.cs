using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Match
    {
        public int Id { get; set; }

        public string? Category { get; set; }

        public string? HomeTeamName { get; set; }
        public string? AwayTeamName { get; set; }

        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public string? Stadium { get; set; }
        public string? MatchStatus { get; set; }

        // Foreign Keys
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        // Navigation Properties (كل واحدة مرتبطة بـ ID مختلف)
        [ForeignKey(nameof(HomeTeamId))]
        public Team? HomeTeam { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public Team? AwayTeam { get; set; }

        public string HomeTeamScore { get; set; } = "0";
        public string AwayTeamScore { get; set; } = "0";

        public ICollection<PlayersReport>? PlayersReports { get; set; }
        public ICollection<GoalsReports>? GoalsReports { get; set; } 
        public ICollection<StaffReport>? StaffReports { get; set; } 
        public ICollection<CardsReports>? CardsReports { get; set; } 

    }
}
