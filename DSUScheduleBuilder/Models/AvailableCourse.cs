using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Models
{
    public class AvailableCourse
    {
        public string SectionID;
        public bool Open;
        public string AcademicLevel;
        public string CourseID;
        public string Description;
        public string CourseName;
        public string StartDate;
        public string EndDate;
        public string Location;
        public string MeetingInformation;
        public string Supplies;
        public int Credits;
        public int SlotsAvailable;
        public int SlotsCapacity;
        public int SlotsWaitlist;
        public int TimeStart;
        public int TimeEnd;
        public string ProfessorEmails;
        public List<string> Teacher;
        public string PrereqNonCourse;
        public string RecConcurrentCourses;
        public string ReqConcurrentCourses;
        public string PrereqCoursesAnd;
        public string PrereqCoursesOr;
        public string InstructionalMethods;
        public string Term;
        public string DaysOfWeek;
        public int Key;

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
