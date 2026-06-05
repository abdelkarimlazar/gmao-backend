using GmaoIntentApi.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GmaoIntentApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController
        : ControllerBase
    {
        private readonly INotificationService
            _notificationService;

        public NotificationsController(
            INotificationService
                notificationService)
        {
            _notificationService =
                notificationService;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetNotifications()
        {
            var notifications =
                await _notificationService
                    .GetAllAsync();

            return Ok(notifications);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult>
            MarkAsRead(int id)
        {
            await _notificationService
                .MarkAsReadAsync(id);

            return NoContent();
        }
    }
}