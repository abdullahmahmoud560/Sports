using Microsoft.EntityFrameworkCore;

namespace Sports.DTO
{
    public class RegisterDTO
    {
        public string? AcademyName { get; set; }
        public string? AcademyEmail { get; set; }
        public string? AcademyPhone { get; set; }
        public string? AcademyCity { get; set; }
        public string? AcademyCountry { get; set; }
        public string? AcademyPassword { get; set; }
        public string? LogoURL { get; set; }
        public string? Coordinator { get; set; }

    }
}
