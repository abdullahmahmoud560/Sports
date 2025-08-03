using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class CardsReports
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } =string.Empty; // اسم اللاعب
        public string AcadamyName { get; set; } =string.Empty; // اسم الأكاديمية
        public string CardType { get; set; } = string.Empty; // نوع البطاقة: "Yellow" أو "Red"
        public string? Notes { get; set; } // ملاحظات إضافية (اختياري)

        public Match? Match { get; set; }
        [ForeignKey(nameof(Match))]
        public int MatchId { get; set; }

    }

}
