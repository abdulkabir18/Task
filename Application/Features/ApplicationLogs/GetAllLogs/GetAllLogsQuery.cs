using Application.DTOs.ApplicationLog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ApplicationLogs.GetAllLogs
{
    public class GetAllLogsQuery : IRequest<List<AppLogDto>> { }
}
