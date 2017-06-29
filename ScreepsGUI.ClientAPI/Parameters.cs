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
            }

            public static string BaseURL { get; set; }

            public static string Auth_Signin { get { return BaseURL + "/auth/signin"; } }
            public static string Auth_AboutMe { get { return BaseURL + "/auth/me"; } }
        }
    }
}