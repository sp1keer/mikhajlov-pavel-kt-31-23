using System.Text.Json.Serialization;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int GroupId { get; set; } // Внешний ключ

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public Group? Group { get; set; } // Связь с таблицей Groups

        [JsonIgnore]
        public ICollection<Grade>? Grades { get; set; } // Связь с таблицей Grades
    }
}
