using Sports.Model;

namespace Sports.DTO
{
    public class MatchesTable
    {
            public int Id { get; set; }
            public int HomeAcademyId { get; set; }
            public int AwayAcademyId { get; set; }
            public int HomeGoals { get; set; }
            public int AwayGoals { get; set; }
            public Academy? HomeAcademy { get; set; }
            public Academy? AwayAcademy { get; set; }
    }
}
