using System.Diagnostics;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; } // Внешний ключ
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        public Group Group { get; set; } // Связь с таблицей Groups
        public ICollection<Grade> Grades { get; set; } // Связь с таблицей Grades
    }
}
