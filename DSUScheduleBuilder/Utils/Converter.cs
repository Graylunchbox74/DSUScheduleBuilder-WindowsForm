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
            if (minute.Length == 1) minute = "0" + minute;
            
            return hour + ":" + minute;
        }

        public static int IntTimeFromString(string s)
        {
            return 0;
        }

        public static string IntersperseNewLines(string s)
        {
            StringBuilder sb = new StringBuilder();
            string[] words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (i != 0 && i % 7 == 0) sb.Append("\n");
                sb.Append(words[i]);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
