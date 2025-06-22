namespace Sports.DTO
{
    public class UpdateAcademyDTO
    {
        public string? AdditionalPhoneNumber { get; set; }
        public string? AdditionalEmail { get; set; }
        public string TShirtColor { get; set; } = string.Empty;
        public string ShortColor { get; set; } = string.Empty;
        public string ShoesColor { get; set; } = string.Empty;
        public bool? under12 { get; set; } = false;
        public bool? under14 { get; set; } = false;
        public bool? under16 { get; set; } = false;
        public string? AdditionalTShirtColor { get; set; } = string.Empty;
        public string? AdditionalShortColor { get; set; } = string.Empty;
        public string? AdditionalShoesColor { get; set; } = string.Empty;
    }
}
