using Sports.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DTO
{
    public class GetPlayersDTO
    {
        public int PlayerID { get; set; }
        public string PLayerName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string? BirthDate { get; set; }
        public string? Possition { get; set; }
        public string NumberShirt { get; set; } = string.Empty;
        public string URLImage { get; set; } = string.Empty;
        public string URLPassport { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public string? AcademyName { get; set; } = string.Empty;

    }
}
