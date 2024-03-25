using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace LR11_ASP
{
    public class LogActionFilter : IActionFilter
    {
        private readonly string _logFilePath;

        public LogActionFilter(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string methodName = context.ActionDescriptor.DisplayName;
            string logMessage = $"{DateTime.UtcNow} - Executing method: {methodName}";

            WriteToLog(logMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }

        private void WriteToLog(string logMessage)
        {
            using (StreamWriter sw = File.AppendText(_logFilePath))
            {
                sw.WriteLine(logMessage);
            }
        }
    }
}
