using System.Text.Json.Serialization;

namespace mikhajlov_pavel_kt_31_23.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        // Навигационное свойство (1 ко многим с Groups)
        [JsonIgnore]
        public ICollection<Group>? Groups { get; set; }
    }
}
