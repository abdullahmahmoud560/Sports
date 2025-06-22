using System.ComponentModel.DataAnnotations;

namespace Sports.Model
{
    public class Academy
    {
        public int Id { get; set; }
        public string? academyManagerName { get; set; } = string.Empty;
        public string AcademyName { get; set; } = string.Empty;
        public string AcademyCountry { get; set; } = string.Empty;
        [EmailAddress]
        public string AcademyEmail { get; set; } = string.Empty;
        [Phone]
        public string AcademyPhone { get; set; } = string.Empty;
        public string AcademyPassword { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        [Phone]
        public string? AdditionalPhoneNumber { get; set; }
        [EmailAddress]
        public string? AdditionalEmail { get; set; }
        public bool? under12 { get; set; } = false;
        public bool? under14 { get; set; } = false;
        public bool? under16 { get; set; } = false;
        public bool Statue { get; set; } = false;
        public string Role { get; set; } = "Academy";
        public string TShirtColor { get; set; } = string.Empty;
        public string ShortColor { get; set; } = string.Empty;
        public string ShoesColor { get; set; } = string.Empty;
        public string? AdditionalTShirtColor { get; set; } = string.Empty;
        public string? AdditionalShortColor { get; set; } = string.Empty;
        public string? AdditionalShoesColor { get; set; } = string.Empty;
    }
}
