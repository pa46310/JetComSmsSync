using System;
using System.Collections.Generic;
using System.Text;

namespace JetComSmsSync.Core.Utils
{
    public static class DateUtils
    {
        public static IEnumerable<Tuple<DateTime, DateTime>> GetRange(DateTime startDate, DateTime endDate, int incrementByMonth = 12)
        {
            var start = startDate;
            var end = start.AddMonths(incrementByMonth);
            if (end > endDate)
            {
                end = endDate;
            }
            while (start < end)
            {
                yield return new Tuple<DateTime, DateTime>(start, end);
                start = end;
                end = start.AddMonths(incrementByMonth);
                if (end > endDate)
                {
                    end = endDate;
                }
            }
        }
    }
}
