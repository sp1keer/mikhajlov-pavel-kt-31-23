using System.Text.Json.Serialization;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Course { get; set; }
        public int SpecialtyId { get; set; } // Внешний ключ

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public Specialty? Specialty { get; set; } // Связь с таблицей Specialtys

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; } // Связь с таблицей Students
    }
}
