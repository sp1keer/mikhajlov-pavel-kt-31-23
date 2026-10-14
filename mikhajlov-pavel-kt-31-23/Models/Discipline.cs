using System.Diagnostics;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        // Навигационное свойство
        public ICollection<Grade> Grades { get; set; } // Связь с таблицей Grades
    }
}
