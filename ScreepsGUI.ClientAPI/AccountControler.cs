﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreepsGUI.DTO;
using ScreepsGUI.DTO.Enum;
using ScreepsGUI.Tools.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreepsGUI.ClientAPI
{
    public static class AccountControler
    {
        public static AuthenticationAnswer Authenticate(string email, string password)
        {
            List<RESTParameter> restParameters = new List<RESTParameter>();

            var body = new { email = email, password = password };

            restParameters.Add(new RESTParameter(RESTParameterType.POST, JsonConvert.SerializeObject(body)));

            RESTResponse restResponse = CallRESTService.CallService(Parameters.URLs.Auth.Signin, restParameters, CallModeServiceREST.POST);

            AuthenticationAnswer authenticationAnswer = new AuthenticationAnswer();

            if (restResponse.Error != null)
            {
                authenticationAnswer.Success = false;

                switch (restResponse.HTTPStatuCode)
                {
                    case 401:
                        authenticationAnswer.ErrorType = AuthenticationErrorType.Unauthorized;
                        break;

                    default:
                        authenticationAnswer.ErrorType = AuthenticationErrorType.Unknow;
                        break;
                }
            }
            else
            {
                authenticationAnswer.Success = restResponse.Body.Contains("token");
            }

            if (authenticationAnswer.Success)
            {
                authenticationAnswer.Token = ((JObject)JsonConvert.DeserializeObject(restResponse.Body)).SelectToken("token").ToObject<string>();

                Context.Token = authenticationAnswer.Token;
            }

            return authenticationAnswer;
        }

        public static void Disconnect()
        {
            Context.Token = string.Empty;
        }

        public static Account GeAccountInformation()
        {
            List<RESTParameter> restParameters = new List<RESTParameter>();

            restParameters.Add(new RESTParameter(RESTParameterType.HEADER, "X-Token", Context.Token));
            restParameters.Add(new RESTParameter(RESTParameterType.HEADER, "X-Username", Context.Token));

            RESTResponse restResponse = CallRESTService.CallService(Parameters.URLs.Auth.AboutMe, restParameters, CallModeServiceREST.GET);

            if (restResponse.Error != null)
            {
                throw new Exception();
            }

            return JsonConvert.DeserializeObject<Account>(restResponse.Body);
        }
    }
}