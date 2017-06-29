using System.Collections.Generic;

namespace ScreepsGUI.Tools.REST
{
    public class RESTCallInformations
    {
        public CallModeServiceREST VERB { get; internal set; }

        public string Domain { get; internal set; }
        public string BaseURL { get; internal set; }
        public string CalledURL { get; internal set; }

        public Dictionary<string, string> Headers { get; internal set; }
        public string Body { get; internal set; }

        public double CallDuration { get; internal set; }
    }
}