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
        //public int uid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string majors { get; set; }
        public string minors { get; set; }
        public string email { get; set; }

        public User ToUser()
        {
            return new User
            {
                //Uid = this.uid,
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
        public int key { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public int credits { get; set; }
        public string classID { get; set; }
        public string className { get; set; }
        public string teacher { get; set; }
        public string location { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string daysOfWeek { get; set; }

        public Course ToCourse()
        {
            return new Course()
            {
                Key = this.key,
                StartTime = this.startTime,
                EndTime = this.endTime,
                StartDate = this.startDate,
                EndDate = this.endDate,
                Credits = this.credits,
                ClassID = this.classID,
                ClassName = this.className,
                Teacher = this.teacher,
                Location = this.location,
                DaysOfWeek = this.daysOfWeek
            };
        }
    }

    class CoursesResponse : Errorable
    {
        public List<SingleCourseResponse> classes { get; set; }

        public List<Course> ToCourses()
        {
            return classes.ConvertAll<Course>((course) => course.ToCourse());
        }
    }
    
    class LoginResponse : Errorable
    {
        public UserResponse user { get; set; }
        public string uuid { get; set; }
    }

    class SuccessResponse : Errorable
    {
        public int success { get; set; }
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
        private string _session_token;

        public HttpRequester(string target)
        {
            if (_default == null)
                _default = this;

            _client = new RestClient(target);
        }

        public void Login(string email, string password, Func<LoginResponse, bool> callback)
        {
            Console.WriteLine("LOGGING IN");
            var postRequest = new RestRequest(Method.POST)
            {
                Resource = "api/user/login"
            };

            postRequest.AddParameter("email", email);
            postRequest.AddParameter("password", password);

            var response = _client.Execute<LoginResponse>(postRequest);

            if (response.Data == null)
            {
                Errors.BadData("Failed to login");
                return;
            }

            bool worked = callback(response.Data);

            if (worked)
            {
                Console.Write("Setting session token: ");

                _session_token = response.Data.uuid;

                Console.WriteLine(_session_token);
            }
        }

        public void Logout()
        {
            Console.WriteLine("LOGGING OUT");
            var postRequest = new RestRequest(Method.POST)
            {
                Resource = "api/user/logout"
            };

            postRequest.AddParameter("uuid", _session_token);
            var response = _client.Execute<SuccessResponse>(postRequest);
        }

        public void NewUser(string email, string password, string first, string last, Func<SuccessResponse, bool> callback)
        {
            Console.WriteLine("CREATING NEW USER");
            var req = new RestRequest(Method.POST)
            {
                Resource = "api/user/new"
            };

            req.AddParameter("email", email);
            req.AddParameter("password", password);
            req.AddParameter("firstName", first);
            req.AddParameter("lastName", last);

            var res = _client.Execute<SuccessResponse>(req);
            if (res.Data == null)
            {
                Errors.BadData("Failed to add new user");
                return;
            }

            callback(res.Data);
        }

        public User GetUser()
        {
            Console.WriteLine("LOADING USER");
            var getRequest = new RestRequest(Method.GET)
            {
                Resource = "api/user/getData/" + _session_token
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
        
        public List<Course> GetPreviousCourses()
        {
            var getRequest = new RestRequest(Method.GET)
            {
                Resource = "api/courses/previous/" + _session_token
            };
            var response = _client.Execute<CoursesResponse>(getRequest);
            CoursesResponse courses = response.Data;

            if (courses == null)
            {
                Errors.BadData("Parsing Previous Courses failed");
                return null;
            }

            if (courses.errorCode != null)
            {
                Errors.Code(courses);
                //Properly handle errors
                return null;
            }

            return courses.ToCourses();
        }

        public List<Course> GetEnrolledCourses()
        {
            var getRequest = new RestRequest(Method.GET)
            {
                Resource = "api/courses/enrolled/" + _session_token
            };
            var response = _client.Execute<CoursesResponse>(getRequest);
            CoursesResponse courses = response.Data;

            if (courses == null)
            {
                Errors.BadData("Parsing Enrolled Courses failed");
                return null;
            }

            if (courses.errorCode != null)
            {
                Errors.Code(courses);
                //Properly handle errors
                return null;
            }

            return courses.ToCourses();
        }
    }
}
