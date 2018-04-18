using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DSUScheduleBuilder.Network {
    class UserResponse {
        public int? uid { get; set; }
        public string name { get; set; }
        public string major { get; set; }

        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public string errorLocation { get; set; }
    }

    class HttpRequester {
        public static void getUser(string name) {
            var rc = new RestClient("http://localhost:4200");
            var getRequest = new RestRequest(Method.GET);
            getRequest.Resource = "user/" + name;

            var response = rc.Execute<UserResponse>(getRequest);
            UserResponse user = response.Data;
            if (user.errorCode != null) {
                Console.WriteLine("ERROR");
            } else {
                Console.WriteLine(response.Data.name, response.Data.major);
            }
        }
    }
}
