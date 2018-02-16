using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSUScheduleBuilderServer
{
    class Student
    {
        private string _name;
        public  string Name
        {
            get
            {
                return _name;
            }
        }

        private List<Course> _plannedClasses;
        private List<Course> _takenClasses;
        private string _major;
        private int _catalogYear;
    }
}
