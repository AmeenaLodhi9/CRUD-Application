using EF_CRUD_Application.Data; // Ensure this is correct
using EF_CRUD_Application.Models;
using System;

namespace EF_CRUD_Application
{
    public class Logger
    {
        private static readonly Logger _instance = new Logger();
        private static readonly object _lock = new object(); // For thread-safety

        // Private constructor to prevent direct instantiation
        private Logger()
        {
            // Initialization code if needed
        }

        // Public property to access the singleton instance
        public static Logger Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance;
                }
            }
        }

        public void Log(string message, string stackTrace)
        {
            using (var context = new AppDbContext())
            {
                var log = new Log
                {
                    Message = message,
                    StackTrace = stackTrace,
                    CurrentTime = DateTime.Now
                };

                context.Logs.Add(log);
                context.SaveChanges();
            }
        }
    }
}
