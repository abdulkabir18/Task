using Application.Abstractions;
using Application.DTOs.ApplicationLog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicationLogs.GetAllLogs
{
    public class GetAllLogsHandler : IRequestHandler<GetAllLogsQuery, List<AppLogDto>>
    {
        private readonly ILogRepository _logRepository;

        public GetAllLogsHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<AppLogDto>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await  _logRepository.GetAllAsync();

            return logs.Select(l => new AppLogDto
            {
                Id = l.Id,
                Message = l.Message,
                Level = l.Level,
                Timestamp = l.Timestamp,
                Exception = l.Exception
            }).ToList();
        }
    }

}
