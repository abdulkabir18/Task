using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    using Application.Abstractions;
    using Domain.Entities;
    using Infrastructures.DBaseContext;
    using Microsoft.EntityFrameworkCore;

    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task<(List<TaskItem>, int)> GetCompletedTasksAsync(Guid userId, int page, int size)
        {
            var query = _context.Tasks
                .Where(t => t.UserId == userId && t.Status == Domain.Enums.TaskStatus.Completed);

            var count = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.DueDate)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (items, count);
        }

        public async Task<(List<TaskItem>, int)> GetAllByUserAsync(Guid userId, int page, int size)
        {
            var query = _context.Tasks
                .Where(t => t.UserId == userId);

            var count = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.DueDate)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (items, count);
        }
    }


}
