using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Models
{
    public class AvailableCourse : Course
    {
        public string SectionID;
        public bool Open;
        public string AcademicLevel;
        public string Description;
        public string MeetingInformation;
        public string Supplies;
        public int SlotsAvailable;
        public int SlotsCapacity;
        public int SlotsWaitlist;
        public string ProfessorEmails;
        public string PrereqNonCourse;
        public string RecConcurrentCourses;
        public string ReqConcurrentCourses;
        public string PrereqCoursesAnd;
        public string PrereqCoursesOr;
        public string InstructionalMethods;
        public string Term;

    }
}
