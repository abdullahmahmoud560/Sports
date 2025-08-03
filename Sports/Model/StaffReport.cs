using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class StaffReport
    {
        public int Id { get; set; }
        public string TechName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // بدل attribute
        public string? Notes { get; set; } = string.Empty;
        public Match? Match { get; set; }
        [ForeignKey(nameof(Match))]
        public int MatchId { get; set; }
    }
}
