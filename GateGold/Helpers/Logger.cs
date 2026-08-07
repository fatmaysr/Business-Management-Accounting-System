using System;
using System.IO;
using System.Web;
namespace GateGold.Helpers { 
    public static class Logger { 
        private static readonly object _lock = new object(); 
        public static void LogError(Exception ex, string context = null) {
            try { 
                var logsFolder = HttpContext.Current != null ? HttpContext.Current.Server.MapPath("~/App_Data/Logs") : AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Logs"; lock (_lock) { 
                    if (!Directory.Exists(logsFolder)) Directory.CreateDirectory(logsFolder); var fileName = Path.Combine(logsFolder, $"log-{DateTime.Now:yyyy-MM-dd}.txt"); var entry = $"---------------------------------------------{Environment.NewLine}" + $"Tarih : {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}" + (string.IsNullOrEmpty(context) ? "" : $"Bağlam : {context}{Environment.NewLine}") + $"Hata Tipi : {ex.GetType()}{Environment.NewLine}" + $"Mesaj : {ex.Message}{Environment.NewLine}" + $"Stack Trace : {ex.StackTrace}{Environment.NewLine}"; if (ex.InnerException != null) entry += $"İç Hata : {ex.InnerException.Message}{Environment.NewLine}"; File.AppendAllText(fileName, entry); } }
            
            
            catch 
            {
            }
        }
    }
}