using LogMessageSenderApi3.Data;
using LogMessageSenderApi3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LogMessageSenderApi3.Services
{
    public interface ILogService
    {
        int SaveLog(string message);
        List<LogMessage> GetLogs();
    }

    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int SaveLog(string message)
        {
            if (message.IsNullOrEmpty())
            {
                var errorMessage = new LogMessage { Message = "ERR: message cannot be empty" };
                _context.Logs.Add(errorMessage);
                return _context.SaveChanges();
            }

            var logMessage = new LogMessage { Message = message, Timestamp = DateTime.UtcNow};
            _context.Logs.Add(logMessage);
            return _context.SaveChanges();
        }

        public List<LogMessage> GetLogs()
        {
            return _context.Logs.ToList();
        }
    }

}
