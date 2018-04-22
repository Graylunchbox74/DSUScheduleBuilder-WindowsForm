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
            if (classes == null) return null;

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

    class AvailableCourseResponse
    {
        public string sectionID { get; set; }
        public bool open { get; set; }
        public string academicLevel { get; set; }
        public string courseID { get; set; }
        public string description { get; set; }
        public string courseName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string location { get; set; }
        public string meetingInformation { get; set; }
        public string supplies { get; set; }
        public int credits { get; set; }
        public int slotsAvailable { get; set; }
        public int slotsCapacity { get; set; }
        public int slotsWaitlist { get; set; }
        public int timeStart { get; set; }
        public int timeEnd { get; set; }
        public string professorEmails { get; set; }
        public List<string> teacher { get; set; }
        public string prereqNonCourse { get; set; }
        public string recConcurrentCourses { get; set; }
        public string reqConcurrentCourses { get; set; }
        public string prereqCoursesAnd { get; set; }
        public string prereqCoursesOr { get; set; }
        public string instructionalMethods { get; set; }
        public string term { get; set; }
        public int key { get; set; }

        public AvailableCourse ToAvailableCourse()
        {
            return new AvailableCourse()
            {
                SectionID = sectionID,
                Open = open,
                AcademicLevel = academicLevel,
                CourseID = courseID,
                Description = description,
                CourseName = courseName,
                StartDate = startDate,
                EndDate = endDate,
                Location = location,
                MeetingInformation = meetingInformation,
                Supplies = supplies,
                Credits = credits,
                SlotsAvailable = slotsAvailable,
                SlotsCapacity = slotsCapacity,
                SlotsWaitlist = slotsWaitlist,
                TimeStart = timeStart,
                TimeEnd = timeEnd,
                ProfessorEmails = professorEmails,
                Teacher = teacher,
                PrereqNonCourse = prereqNonCourse,
                RecConcurrentCourses = recConcurrentCourses,
                ReqConcurrentCourses = reqConcurrentCourses,
                PrereqCoursesAnd = prereqCoursesAnd,
                PrereqCoursesOr = prereqCoursesOr,
                InstructionalMethods = instructionalMethods,
                Term = term,
                Key = key
            };
        }
    }

    class FullAvailableCourseResponse : Errorable
    {
        public List<AvailableCourseResponse> classes { get; set; }

        public List<AvailableCourse> ToCourses()
        {
            return classes?.ConvertAll<AvailableCourse>((AvailableCourseResponse acr) => acr.ToAvailableCourse());
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
                Resource = "api/user/newUser"
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

        public List<AvailableCourse> GetAvailableCourses()
        {
            var req = new RestRequest(Method.GET)
            {
                Resource = "api/courses/available/" + _session_token
            };
            var res = _client.Execute<FullAvailableCourseResponse>(req);
            FullAvailableCourseResponse courses = res.Data;

            if (courses == null)
            {
                Errors.BadData("Parsing Available courses failed");
                return null;
            }

            if (courses.errorCode != null)
            {
                Errors.Code(courses);
                return null;
            }

            return courses.ToCourses();
        }

        public List<AvailableCourse> SearchForCourses(string term, string prefix, string number, string ilastname, Func<FullAvailableCourseResponse, bool> callback)
        {
            var req = new RestRequest(Method.GET)
            {
                Resource = "api/courses/search/" + _session_token
            };

            if (term != "") req.AddParameter("term", term);
            if (prefix != "") req.AddParameter("prefix", prefix);
            if (number != "") req.AddParameter("number", number);
            if (ilastname != "") req.AddParameter("instructor", ilastname);

            var res = _client.Execute<FullAvailableCourseResponse>(req);
            FullAvailableCourseResponse courses = res.Data;

            if (courses == null)
            {
                Errors.BadData("Parsing searched courses failed");
                return null;
            }

            if (callback(courses))
            {
                return courses?.ToCourses();
            }
            else
            {
                return null;
            }
        }
    }
}
