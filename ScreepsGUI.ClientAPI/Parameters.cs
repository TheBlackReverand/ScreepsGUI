using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreepsGUI.ClientAPI
{
    public static class Parameters
    {
        public static class URLs
        {
            static URLs()
            {
                BaseURL = "https://screeps.com/api";


                // https://screeps.com/ptr/api;
            }

            public static string BaseURL { get; set; }

            public static class Auth
            {
                public static string Signin { get { return BaseURL + "/auth/signin"; } }
                public static string AboutMe { get { return BaseURL + "/auth/me"; } }
            }
            public static class User
            {
                public static string Find { get { return BaseURL + "/user/find?username={username}"; } }
            }
        }
    }
}