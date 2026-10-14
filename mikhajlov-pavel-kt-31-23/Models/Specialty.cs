using System.Text.RegularExpressions;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        // Навигационное свойство (1 ко многим с Groups)
        public ICollection<Group> Groups { get; set; }
    }
}
