using GmaoIntentApi.Enums;

namespace GmaoIntentApi.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EquipmentStatus Status { get; set; }
    }
}
