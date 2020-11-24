using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Dapper.V1.DTO.EventsLog.CreateEventLog
{
    public class CreateEventLogDto
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTimeOffset EventAt { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
