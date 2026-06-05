namespace GmaoIntentApi.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalUsers { get; set; }
        public int TotalEquipments { get; set; }
        public int ActiveEquipments { get; set; }
        public int BrokenEquipments { get; set; }
        public int TotalMaintenanceTasks { get; set; }
        public int PendingTasks { get; set; }
        public int InProgressTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int OpenBreakdowns { get; set; }

        public int DoneTasks { get; set; }
        public int TotalBreakdowns { get; set; }
        public int PendingBreakdowns { get; set; }
        public int InProgressBreakdowns { get; set; }
        public int ResolvedBreakdowns { get; set; }
    }
}
