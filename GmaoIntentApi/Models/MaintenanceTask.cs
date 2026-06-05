using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.Models
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public int AssignedTo { get; set; }
        public User User { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GmaoTaskStatus Status { get; set; }
        public string Priority { get; set; } = string.Empty;
    }
}
