using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreepsGUI.DTO;
using ScreepsGUI.Tools.REST;
using System.Collections.Generic;

namespace ScreepsGUI.ClientAPI
{
    public static class UserControler
    {
        public static SearchUserResult SearchUser(string username)
        {
            string url = Parameters.URLs.User.Find;

            url = url.Replace("{username}", username);

            RESTResponse restResponse = CallRESTService.CallService(url, new List<RESTParameter>(), CallModeServiceREST.GET);

            SearchUserResult searchUserResult = new SearchUserResult();

            if (restResponse.Error != null)
            {
                searchUserResult.Success = false;
            }
            else
            {
                searchUserResult.Success = restResponse.Body.Contains("username");
            }

            if (searchUserResult.Success)
            {
                searchUserResult.UserFound = ((JObject)JsonConvert.DeserializeObject(restResponse.Body)).SelectToken("user").ToObject<Account>();
            }

            return searchUserResult;
        }
    }
}