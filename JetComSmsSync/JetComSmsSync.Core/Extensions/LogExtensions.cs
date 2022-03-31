using JetComSmsSync.Core;
using System;
using System.Windows;

namespace Serilog
{
    public static class LogExtensions
    {
        public static void LogAndShowError(this ILogger log, Exception ex, string caption)
        {
            log.Error(ex, caption);
            MessageService.Instance.EnqueInformation(caption + ":" + ex.Message);
        }

        public static void LogAndShowInfo(this ILogger log, Exception ex, string caption)
        {
            log.Information(ex, caption);
            MessageService.Instance.EnqueInformation(caption + ":" + ex.Message);
        }
    }
}
