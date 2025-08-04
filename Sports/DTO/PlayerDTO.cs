namespace Sports.DTO
{
    public class PlayerDTO
    {
        public string? PLayerName { get; set; }
        public string? Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Possition { get; set; }
        public string? NumberShirt { get; set; }
        public IFormFile? URLImage { get; set; }
        public IFormFile? URLPassport { get; set; }
        public string? category { get; set; }
    }
}
