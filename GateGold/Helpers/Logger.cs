using System;
using System.IO;
using System.Text;
using System.Web;

namespace GateGold.Helpers
{
    public static class Logger
    {
        private static readonly string LogFolderPath = HttpContext.Current != null
            ? HttpContext.Current.Server.MapPath("~/App_Data/Logs")
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Logs");

        /// <summary>
        /// Olay veya hata mesajlarını log dosyasına yazar ve eski logları temizler.
        /// </summary>
        public static void LogError(Exception ex, string requestUrl = null)
        {
            try
            {
                // Log klasörü yoksa oluştur
                if (!Directory.Exists(LogFolderPath))
                {
                    Directory.CreateDirectory(LogFolderPath);
                }

                // Günlük log dosyası ismi (Örn: log_2026-08-10.txt)
                string fileName = $"log_{DateTime.Now:yyyy-MM-dd}.txt";
                string filePath = Path.Combine(LogFolderPath, fileName);

                var logBuilder = new StringBuilder();
                logBuilder.AppendLine("==================================================");
                logBuilder.AppendLine($"Tarih / Saat : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                if (!string.IsNullOrEmpty(requestUrl))
                {
                    logBuilder.AppendLine($"Request URL  : {requestUrl}");
                }
                logBuilder.AppendLine($"Hata Mesajı  : {ex?.Message}");
                logBuilder.AppendLine($"Hata Kaynağı : {ex?.Source}");
                logBuilder.AppendLine($"Stack Trace  : {ex?.StackTrace}");
                if (ex?.InnerException != null)
                {
                    logBuilder.AppendLine($"Inner Ex     : {ex.InnerException.Message}");
                }
                logBuilder.AppendLine("==================================================");
                logBuilder.AppendLine();

                // Dosyaya yaz
                File.AppendAllText(filePath, logBuilder.ToString(), Encoding.UTF8);

                // Eski logları temizle (30 günden eski)
                CleanOldLogs(30);
            }
            catch
            {
                // Loglama sırasındaki hataların uygulamayı patlatmasını engelle
            }
        }

        /// <summary>
        /// Belirtilen günden eski log dosyalarını diskten siler.
        /// </summary>
        private static void CleanOldLogs(int retentionDays)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(LogFolderPath);
                FileInfo[] files = directoryInfo.GetFiles("log_*.txt");

                DateTime thresholdDate = DateTime.Now.AddDays(-retentionDays);

                foreach (var file in files)
                {
                    if (file.CreationTime < thresholdDate)
                    {
                        file.Delete();
                    }
                }
            }
            catch
            {
                // Rotasyon hatası uygulamayı durdurmasın
            }
        }
    }
}