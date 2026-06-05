namespace GmaoIntentApi.Models
{
    public class InterventionHistory
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public MaintenanceTask Task { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Action { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
