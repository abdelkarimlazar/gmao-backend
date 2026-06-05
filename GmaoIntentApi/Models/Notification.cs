namespace GmaoIntentApi.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }
            = string.Empty;

        public DateTime CreatedAt
        { get; set; }
            = DateTime.UtcNow;

        public DateTime? ReadAt
        { get; set; }
    }
}