using Mahzan.Dapper.V1.DTO.EventsLog.CreateEventLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.V1.Repositories._Base.EventsLog
{
    public interface IEventsLogRepository
    {
        Task Create(CreateEventLogDto createEventLogDto);
    }
}
