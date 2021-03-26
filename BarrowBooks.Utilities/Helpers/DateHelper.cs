using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Utilities.Helpers
{
    public static class DateHelper
    {
        // excluded public holidays.
        public static readonly List<DateTime> excludeHolidayDates = new List<DateTime>()
        {
            DateTime.ParseExact("25/03/2021", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
        };

        public static int DaysLeft(DateTime startDate, DateTime endDate, Boolean excludeWeekends)
        {
            int count = 0;
            for (DateTime index = startDate; index <= endDate; index = index.AddDays(1))
            {
                if (excludeWeekends && (index.DayOfWeek == DayOfWeek.Sunday || index.DayOfWeek == DayOfWeek.Saturday))
                    continue;

                if (excludeHolidayDates.Contains(index.Date))
                    continue;

                count++;
            }
            return count;
        }
    }
}
