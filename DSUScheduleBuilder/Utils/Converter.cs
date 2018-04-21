using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Utils
{
    public static class Converter
    {
        /// <summary>
        /// Converts a time stored in an int, i.e. 1245, to the number of minutes after midnight corresponding to the time.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeIntToMinutes(int time)
        {
            return (time / 100) * 60 + (time % 100);
        }

        public static string TimeIntToString(int time)
        {
            string hour = (time / 100).ToString();
            string minute = (time % 100).ToString();
            return hour + ":" + minute;
        }
    }
}
