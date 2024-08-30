using EF_CRUD_Application.Data; // Ensure this is correct
using EF_CRUD_Application.Models;
using System;

namespace EF_CRUD_Application
{
    public static class Logger
    {
        public static void Log(string message, string stackTrace)
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
