namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; } // Внешний ключ
        public bool IsDeleted { get; set; }

        // Навигационные свойства
        public Specialty Specialty { get; set; } // Связь с таблицей Specialtys
        public ICollection<Student> Students { get; set; } // Связь с таблицей Students
    }
}
