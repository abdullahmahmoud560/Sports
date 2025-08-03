namespace Sports.DTO
{
    public class MatchReportDto
    {
        public int MatchId { get; set; }

        public List<PlayerReportDto> Players { get; set; } = new();
        public List<GoalReportDto> Goals { get; set; } = new();
        public List<CardReportDto> Cards { get; set; } = new();
        public List<StaffReportDto> Staff { get; set; } = new();
    }

    public class PlayerReportDto
    {
        public string PlayerName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Essential { get; set; } = string.Empty;
        public string? Reserve { get; set; }
        public string? Notes { get; set; }
    }

    public class GoalReportDto
    {
        public string PlayerName { get; set; } = string.Empty;
        public string AcadamyName { get; set; } = string.Empty;
        public string Minute { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    public class CardReportDto
    {
        public string PlayerName { get; set; } = string.Empty;
        public string AcadamyName { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;  // "Yellow" or "Red"
        public string? Notes { get; set; }
    }

    public class StaffReportDto
    {
        public string TechName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

}
