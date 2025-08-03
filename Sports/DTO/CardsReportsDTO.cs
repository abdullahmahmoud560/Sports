namespace Sports.DTO
{
    public class CardsReportsDTO
    {
        public string PlayerName { get; set; } = string.Empty; // اسم اللاعب
        public string AcadamyName { get; set; } = string.Empty; // اسم الأكاديمية
        public string CardType { get; set; } = string.Empty; // نوع البطاقة: "Yellow" أو "Red"
        public string? Notes { get; set; }
    }
}
