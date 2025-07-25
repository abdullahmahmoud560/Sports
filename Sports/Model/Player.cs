﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Player
    {
        public int id { get; set; }
        public string PLayerName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? Possition { get; set; }
        public string NumberShirt { get; set; } = string.Empty;
        public string URLImage { get; set; } = string.Empty;
        public string URLPassport { get; set; } = string.Empty;
        public bool Statu { get; set; } = false;
        public string category { get; set; } = string.Empty;
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
    }
}
