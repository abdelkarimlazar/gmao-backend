using System.ComponentModel.DataAnnotations;
using GmaoIntentApi.Enums;

namespace GmaoIntentApi.DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
            = string.Empty;

        [Required]
        public EquipmentStatus Status { get; set; }
    }

    public class CreateEquipmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
            = string.Empty;

        [Required]
        public EquipmentStatus Status { get; set; }
    }

    public class UpdateEquipmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }
            = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
            = string.Empty;

        [Required]
        public EquipmentStatus Status { get; set; }
    }
}