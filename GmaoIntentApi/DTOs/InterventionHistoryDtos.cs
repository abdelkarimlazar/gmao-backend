using System.ComponentModel.DataAnnotations;

namespace GmaoIntentApi.DTOs
{
    public class InterventionHistoryDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Comment { get; set; } = string.Empty;
    }

    public class CreateInterventionHistoryDto
    {
        [Range(1, int.MaxValue)]
        public int TaskId { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }

    public class UpdateInterventionHistoryDto
    {
        [Range(1, int.MaxValue)]
        public int TaskId { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
