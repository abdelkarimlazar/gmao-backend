using GmaoIntentApi.Enums;
using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.DTOs
{
    public class UserQueryDto : PaginationQueryDto
    {
        public string? Search { get; set; }
    }

    public class EquipmentQueryDto : PaginationQueryDto
    {
        public EquipmentStatus? Status { get; set; }
    }

    public class MaintenanceTaskQueryDto : PaginationQueryDto
    {
        public GmaoTaskStatus? Status { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
    }
}
