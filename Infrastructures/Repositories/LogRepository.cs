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

    public class LogRepository : ILogRepository
    {
        private readonly AppDbContext _context;

        public LogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AppLog log)
        {
            await _context.ApplicationLogs.AddAsync(log);
        }

        public async Task<List<AppLog>> GetAllAsync()
        {
            return await _context.ApplicationLogs
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }
    }


}
