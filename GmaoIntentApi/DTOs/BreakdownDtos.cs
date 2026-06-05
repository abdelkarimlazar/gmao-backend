using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GmaoIntentApi.Enums;

namespace GmaoIntentApi.DTOs
{
    public class BreakdownDto
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public int ReportedBy { get; set; }
        public string ReportedByName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime Date { get; set; }

        public BreakdownStatus Status { get; set; }
    }

    public class CreateBreakdownDto
    {
        [Range(1, int.MaxValue)]
        public int EquipmentId { get; set; }

        [Range(1, int.MaxValue)]
        public int ReportedBy { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime Date { get; set; }

        [Required]
        public BreakdownStatus Status { get; set; }
    }

    public class UpdateBreakdownDto
    {
        [Range(1, int.MaxValue)]
        public int EquipmentId { get; set; }

        [Range(1, int.MaxValue)]
        public int ReportedBy { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime Date { get; set; }

        [Required]
        public BreakdownStatus Status { get; set; }
    }
}
