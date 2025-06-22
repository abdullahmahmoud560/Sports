namespace Sports.DTO
{
    public class RegisterDTO
    {
        public string? academyManagerName { get; set; } = string.Empty;
        public string? AcademyName { get; set; } = string.Empty;
        public string? AcademyCountry { get; set; } = string.Empty;
        public string? AcademyEmail { get; set; } = string.Empty;
        public string? AcademyPhone { get; set; } = string.Empty;
        public IFormFile? LogoURL { get; set; } = null!;
        public string? Password {  get; set; } = string.Empty;
        public string? ConfirmPassword {  get; set; } = string.Empty;

    }
}
