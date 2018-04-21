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
        public string ClassID; //CSC-150
        public string ClassName;
        public string Teacher;
        public string Location;
        public string DaysOfWeek;
    }
}
