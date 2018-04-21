using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using DSUScheduleBuilder.Models;

namespace DSUScheduleBuilder.Network {
    static class Errors
    {
        public static void BadData(string msg)
        {
            Console.WriteLine("[Bad Data] " + msg);
        }

        public static void Code(Errorable error)
        {
            Console.WriteLine("[Error " + error.errorCode + "] " + error.errorMessage);
        }
    }

    class Errorable
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
    }

    //Many of the following fields do not have to be public
    //because they are transformed into another object type before
    //they are returned.
    class UserResponse : Errorable
    {
        public int uid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string majors { get; set; }
        public string minors { get; set; }
        public string email { get; set; }

        public User ToUser()
        {
            return new User
            {
                Uid = this.uid,
                FirstName = this.fname,
                LastName = this.lname,
                Majors = this.majors,
                Minors = this.minors,
                Email = this.email
            };
        }
    }

    class SingleCourseResponse : Errorable
    {
        public string uid;
        public int startTime;
        public int endTime;
        public int credits;
        public string classID;
        public string className;
        public string teacher;
        public string location;
        public string startDate;
        public string endDate;

        public Course ToCourse()
        {
            return new Course()
            {
                Uid = this.uid,
                StartTime = this.startTime,
                EndTime = this.endTime,
                StartDate = this.startDate,
                EndDate = this.endDate,
                Credits = this.credits,
                ClassID = this.classID,
                ClassName = this.className,
                Teacher = this.teacher,
                Location = this.location
            };
        }
    }

    class CoursesResponse : Errorable
    {
        public List<SingleCourseResponse> classes;

        public List<Course> ToCourses()
        {
            return classes.ConvertAll<Course>((course) => course.ToCourse());
        }
    }

    class HttpRequester
    {
        //STATIC FIELD
        //Used to keep the internal code cleaner because we don't have to pass
        //around a HttpRequester instance. Also, we will never need more than one HttpRequester.
        private static HttpRequester _default;
        public static HttpRequester Default
        {
            get
            {
                return _default;
            }
        }


        //CLASS FIELDS
        private RestClient _client;

        public HttpRequester(string target)
        {
            if (_default == null)
                _default = this;

            _client = new RestClient(target);
        }

        public User GetUser(string uuid)
        {
            Console.WriteLine("LOADING USER " + uuid);
            var getRequest = new RestRequest(Method.GET)
            {
                Resource = "api/user/getData/" + uuid
            };
            var response = _client.Execute<UserResponse>(getRequest);
            UserResponse user = response.Data;

            if (user == null)
            {
                Errors.BadData("Parsing User failed");
                return null;
            }

            if (user.errorCode != null)
            {
                Errors.Code(user);
                //Handle the different error codes for a user here
                return null;
            }

            return user.ToUser();
        }

        public List<Course> GetPreviousCourses(string uuid)
        {
            var getRequest = new RestRequest(Method.GET)
            {
                Resource = "api/courses/previous/" + uuid
            };
            var response = _client.Execute<CoursesResponse>(getRequest);
            CoursesResponse courses = response.Data;

            if (courses == null)
            {
                Errors.BadData("Parsing Previous Courses failed");
                return null;
            }

            if (courses.errorCode == null)
            {
                Errors.Code(courses);
                //Properly handle errors
                return null;
            }

            return courses.ToCourses();
        }
    }
}
