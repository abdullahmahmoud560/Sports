namespace Sports.DTO
{
    public class GetAcademyDTO
    {
        public class AcademyDTO
        {
            public int Id { get; set; }
            public string AcademyName { get; set; } = string.Empty;
            public string AcademyCountry { get; set; } = string.Empty;
            public string AcademyEmail { get; set; } = string.Empty;
            public string AcademyPhone { get; set; } = string.Empty;
            public string LogoURL { get; set; } = string.Empty;
            public string? AdditionalPhoneNumber { get; set; }
            public string? AdditionalEmail { get; set; }
            public bool Statue { get; set; } = false;
            public string Role { get; set; } = "Academy";
            public bool? Under12 { get; set; } = false;
            public bool? Under14 { get; set; } = false;
            public bool? Under16 { get; set; } = false;
            public string TShirtColor { get; set; } = string.Empty;
            public string ShortColor { get; set; } = string.Empty;
            public string ShoesColor { get; set; } = string.Empty;
            public string? AdditionalTShirtColor { get; set; } = string.Empty;
            public string? AdditionalShortColor { get; set; } = string.Empty;
            public string? AdditionalShoesColor { get; set; } = string.Empty;
            public string? academyManagerName { get; set; } = string.Empty;

        }
    }
}
