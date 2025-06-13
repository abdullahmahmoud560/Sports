using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
    }
}
