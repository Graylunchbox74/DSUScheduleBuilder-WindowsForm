using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Models
{
    public class Course
    {
        public int Key;
        public int StartTime;
        public int EndTime;
        public string StartDate;
        public string EndDate;
        public int Credits;
        public string CourseID; //CSC-150
        public string CourseName;
        public string Teacher;
        public string Location;
        public string DaysOfWeek;
        
        public string DaysOfWeekPresent
        {
            get
            {
                List<string> days = new List<string>();
                if (DaysOfWeek.Contains("|mon|")) days.Add("Mon");
                if (DaysOfWeek.Contains("|tues|")) days.Add("Tues");
                if (DaysOfWeek.Contains("|wed|")) days.Add("Wed");
                if (DaysOfWeek.Contains("|thur|")) days.Add("Thurs");
                if (DaysOfWeek.Contains("|fri|")) days.Add("Fri");

                string ret = "";
                days.ForEach((d) => ret += d + ", ");

                if (ret.Length == 0) return "Online";

                return ret.Substring(0, ret.Length - 2);
            }
        }
    }
}
