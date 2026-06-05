using GmaoIntentApi.Enums;

namespace GmaoIntentApi.Models
{
    public class Breakdown
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public int ReportedBy { get; set; }
        public User User { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public BreakdownStatus Status { get; set; }
    }
}
