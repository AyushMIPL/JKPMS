using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class AuditLogs : BaseEntity
    {
        [Required]
        public string EventType { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string ColumnName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string Url { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Area { get; set; }

        public string IPAddress { get; set; }

        public int RecordId { get; set; }

        public int OldSystemId { get; set; }

        public string Message { get; set; }

        [NotMapped]
        public string UsernameCrby { get; set; }
        [NotMapped]
        public string UsernameModby { get; set; }
    }
}
