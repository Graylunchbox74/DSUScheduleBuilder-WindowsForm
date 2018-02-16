using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSUScheduleBuilderServer
{
    class Course
    {
        private string _courseName;
        public string CourseName
        {
            get { return _courseName; }
        }

        private string _courseCode;
        public string CourseCode
        {
            get { return _courseCode; }
        }

        private string _professor;
        public string Professor
        {
            get { return _professor; }
        }

        private int _startTime;
        public int StartTime
        {
            get { return _startTime; }
        }

        private int _endTime;
        public int EndTime
        {
            get { return _endTime; }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
        }


        private int _credits;
        public int Credits
        {
            get { return _credits; }
        }

        private Course _lab;

        private string _courseDescription;
        public string CourseDescription
        {
            get { return _courseDescription; }
        }

    }
}
