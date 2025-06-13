using System.ComponentModel.DataAnnotations;

namespace Sports.Model
{
    public class Academy
    {
        public int Id { get; set; }
        public string? AcademyName { get; set; }
        [EmailAddress]
        public string? AcademyEmail { get; set; }
        [Phone]
        public string? AcademyPhone { get; set; }
        public string? AcademyCity { get; set; }
        
        public string? AcademyCountry { get; set; }
        public string? AcademyPassword { get; set; }
        public string LogoURL { get; set; } = string.Empty;
        public string? Coordinator { get; set; }
        public string? Statue { get; set; } = "Waiting";
        public string? Role { get; set; } = "Academy";
    }
}
