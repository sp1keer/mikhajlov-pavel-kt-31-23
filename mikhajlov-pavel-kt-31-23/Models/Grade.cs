namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Value { get; set; } // Оценка
        public int StudentId { get; set; } // Внешний ключ
        public int DisciplineId { get; set; } // Внешний ключ

        // Навигационные свойства
        public Student Student { get; set; }
        public Discipline Discipline { get; set; }
    }
}
