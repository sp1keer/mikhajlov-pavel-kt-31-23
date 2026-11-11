using System.Text.Json.Serialization;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        // Навигационное свойство
        [JsonIgnore]
        public ICollection<Grade>? Grades { get; set; } // Связь с таблицей Grades
    }
}
