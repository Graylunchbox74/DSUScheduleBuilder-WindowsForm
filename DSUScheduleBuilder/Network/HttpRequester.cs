using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DSUScheduleBuilder.Network {
    class Errorable {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
    }

    class UserResponse : Errorable {
        public int? uid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string majors { get; set; }
        public string minors { get; set; }
        public string email { get; set; }
    }



    class HttpRequester {
        public static void getUser(string uuid) {
            var rc = new RestClient("http://localhost:4200");
            var getRequest = new RestRequest(Method.GET);
            getRequest.Resource = "api/user/" + uuid;
            var response = rc.Execute<UserResponse>(getRequest);
            UserResponse user = response.Data;
            if (user.errorCode != null) {
                Console.WriteLine("ERROR");
            } else {
                Console.WriteLine(response.Data.lname);
            }
        }

        public static void getPreviousClasses(string uuid) {

        }
    }
}
