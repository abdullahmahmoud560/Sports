using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string TShirtColor { get; set; } = string.Empty;
        public string ShortColor { get; set; } = string.Empty;
        public string ShoesColor { get; set; } = string.Empty;
        public string? AdditionalTShirtColor { get; set; } = string.Empty;
        public string? AdditionalShortColor { get; set; } = string.Empty;
        public string? AdditionalShoesColor { get; set; } = string.Empty;
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public string category { get; set; } = string.Empty;
    }
}
