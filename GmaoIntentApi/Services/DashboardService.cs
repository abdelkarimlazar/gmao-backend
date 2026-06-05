using GmaoIntentApi.Data;
using GmaoIntentApi.DTOs;
using GmaoIntentApi.Enums;
using GmaoIntentApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            return new DashboardSummaryDto
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalEquipments = await _context.Equipments.CountAsync(),
                ActiveEquipments = await _context.Equipments.CountAsync(e => e.Status == EquipmentStatus.Active),
                BrokenEquipments = await _context.Equipments.CountAsync(e => e.Status == EquipmentStatus.Broken),
                TotalMaintenanceTasks = await _context.MaintenanceTasks.CountAsync(),
                PendingTasks = await _context.MaintenanceTasks.CountAsync(t => t.Status == GmaoTaskStatus.Pending),
                InProgressTasks = await _context.MaintenanceTasks.CountAsync(t => t.Status == GmaoTaskStatus.InProgress),
                CompletedTasks = await _context.MaintenanceTasks.CountAsync(t => t.Status == GmaoTaskStatus.Done),
                DoneTasks = await _context.MaintenanceTasks.CountAsync(t => t.Status == GmaoTaskStatus.Done),
                TotalBreakdowns = await _context.Breakdowns.CountAsync(),
                PendingBreakdowns = await _context.Breakdowns.CountAsync(b => b.Status == BreakdownStatus.Pending),
                InProgressBreakdowns = await _context.Breakdowns.CountAsync(b => b.Status == BreakdownStatus.InProgress),
                OpenBreakdowns = await _context.Breakdowns.CountAsync(b => b.Status != BreakdownStatus.Resolved),
                ResolvedBreakdowns = await _context.Breakdowns.CountAsync(b => b.Status == BreakdownStatus.Resolved)
            };
        }
    }
}
