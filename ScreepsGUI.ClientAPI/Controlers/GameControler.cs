using Newtonsoft.Json;
using ScreepsGUI.ClientAPI.DTO;
using ScreepsGUI.DTO;
using ScreepsGUI.DTO.Enum;
using ScreepsGUI.Tools.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreepsGUI.ClientAPI.Controlers
{
    public static class GameControler
    {
        public static class Room
        {
            public static RoomOverview GetRoomOverview(string roomName, Interval interval = Interval.None)
            {
                List<RESTParameter> restParameters = new List<RESTParameter>();

                restParameters.Add(new RESTParameter(RESTParameterType.HEADER, "X-Token", Context.Token));
                restParameters.Add(new RESTParameter(RESTParameterType.HEADER, "X-Username", Context.Token));

                string url = Parameters.URLs.Game.Room.Overview;
                restParameters.Add(new RESTParameter(RESTParameterType.PATH, "{roomName}", roomName));

                if (interval != Interval.None)
                {
                    url = Parameters.URLs.Game.Room.OverviewWithInterval;
                    restParameters.Add(new RESTParameter(RESTParameterType.PATH, "{interval}", ((int)interval).ToString()));
                }

                RESTResponse restResponse = CallRESTService.CallService(url, restParameters, CallModeServiceREST.GET);

                if (restResponse.Error != null)
                {
                    throw new Exception();
                }
                else
                {
                    Tools.CheckBodyResponse(restResponse.Body);

                    return JsonConvert.DeserializeObject<RoomOverview>(restResponse.Body);
                }
            }
        }
    }
}