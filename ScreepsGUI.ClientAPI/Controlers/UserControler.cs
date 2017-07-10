using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreepsGUI.ClientAPI.DTO;
using ScreepsGUI.DTO;
using ScreepsGUI.Tools.REST;
using System.Collections.Generic;

namespace ScreepsGUI.ClientAPI.Controlers
{
    public static class UserControler
    {
        public static SearchUserResult SearchUser(string username)
        {
            List<RESTParameter> restParameters = new List<RESTParameter>();

            restParameters.Add(new RESTParameter(RESTParameterType.PATH, "{username}", username));

            RESTResponse restResponse = CallRESTService.CallService(Parameters.URLs.User.Find, restParameters, CallModeServiceREST.GET);

            SearchUserResult searchUserResult = new SearchUserResult();

            if (restResponse.Error != null)
            {
                searchUserResult.Success = false;
            }
            else
            {
                Tools.CheckBodyResponse(restResponse.Body);

                searchUserResult.UserFound = ((JObject)JsonConvert.DeserializeObject(restResponse.Body)).SelectToken("user").ToObject<UserAccount>();

                searchUserResult.Success = true;
            }

            return searchUserResult;
        }
    }
}