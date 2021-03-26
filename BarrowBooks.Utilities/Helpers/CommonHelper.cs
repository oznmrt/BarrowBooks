using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrowBooks.Utilities.Helpers
{
    public class CommonHelper
    {
        private static readonly decimal coefficient = 0.20M;
        public static decimal calculatePenalty(DateTime transactionDate)
        {
            var totalDiff = (DateTime.Now.Date - transactionDate.Date).Days;

            int x = 0;
            int y = 1, z;

            // calcualte first day penalty
            var totalAmount = x * coefficient;

            // calcualte other days penalty with fibonacci
            for (int i = 1; i < totalDiff; i++)
            {
                z = x + y;
                totalAmount += (z * coefficient);

                x = y;
                y = z;
            }
            return totalAmount;
        }
    }
}
