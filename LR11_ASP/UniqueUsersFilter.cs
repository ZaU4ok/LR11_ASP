using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;


namespace LR11_ASP
{
    public class UniqueUsersFilter : IActionFilter
    {
        private readonly string _logFilePath;
        private HashSet<string> _uniqueUsers = new HashSet<string>();

        public UniqueUsersFilter(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.User.Identity.Name ?? "Anonymous";
            _uniqueUsers.Add(username);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            string logMessage = $"{DateTime.UtcNow} - Unique users count: {_uniqueUsers.Count}";

            WriteToLog(logMessage);
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
