using System.ComponentModel.DataAnnotations;
using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.DTOs
{
    public class MaintenanceTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public int AssignedTo { get; set; }
        public string AssignedUserName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GmaoTaskStatus Status { get; set; }
        public string Priority { get; set; } = string.Empty;
    }

    public class CreateMaintenanceTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int EquipmentId { get; set; }

        [Range(1, int.MaxValue)]
        public int AssignedTo { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public GmaoTaskStatus Status { get; set; }

        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = string.Empty;
    }

    public class UpdateMaintenanceTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int EquipmentId { get; set; }

        [Range(1, int.MaxValue)]
        public int AssignedTo { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public GmaoTaskStatus Status { get; set; }

        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = string.Empty;
    }
}
