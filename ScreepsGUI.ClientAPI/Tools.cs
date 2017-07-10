using ScreepsGUI.ClientAPI.Exceptions;
using System;

namespace ScreepsGUI.ClientAPI
{
    public static class Tools
    {
        public static void CheckBodyResponse(string body)
        {
            if (String.IsNullOrEmpty(body) || !body.Contains("\"ok\": 1"))
            {
                new BadResponseException();
            }
        }
    }
}