namespace Sports.DTO
{
    public class MatchDTO
    {
        public string? Category { get; set; }
        public string? HomeTeamName { get; set; }
        public string? AwayTeamName { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public string? Stadium { get; set; }
        public string? MatchStatus { get; set; }
        public string? HomeTeamScore { get; set; }
        public string? AwayTeamScore { get; set; }
    }
}
