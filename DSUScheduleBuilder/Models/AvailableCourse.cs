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
        public int Key;
    }
}
