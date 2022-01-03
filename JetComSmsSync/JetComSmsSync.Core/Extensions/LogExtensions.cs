using System;
using System.Windows;

namespace Serilog
{
    public static class LogExtensions
    {
        public static void LogAndShowError(this ILogger log, Exception ex, string caption)
        {
            log.Error(ex, caption);
            _ = MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void LogAndShowInfo(this ILogger log, Exception ex, string caption)
        {
            log.Information(ex, caption);
            _ = MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
