using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNB.Web.Models
{
    public class ExceptionLogger
    {
        public int ExceptionId { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime? ExceptionLogTime { get; set; }
    }
}