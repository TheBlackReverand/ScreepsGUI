using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ScreepsGUI.Tools.REST
{
    public class CallRESTService
    {
        public static RESTResponse CallService(string url, List<RESTParameter> parametres, CallModeServiceREST callModeServiceREST)
        {
            return CallService(url, parametres, callModeServiceREST, "application/json; charset=UTF-8", 1000, true);
        }
        public static RESTResponse CallService(string url, List<RESTParameter> parametres, CallModeServiceREST callModeServiceREST, int timeout)
        {
            return CallService(url, parametres, callModeServiceREST, "application/json; charset=UTF-8", timeout, true);
        }
        public static RESTResponse CallService(string url, List<RESTParameter> parametres, CallModeServiceREST callModeServiceREST, string postContentType, int timeout)
        {
            return CallService(url, parametres, callModeServiceREST, postContentType, timeout, true);
        }
        public static RESTResponse CallService(string url, List<RESTParameter> parametres, CallModeServiceREST callModeServiceREST, string postContentType, int timeout, bool enableLog)
        {
            RESTResponse restResponse = new RESTResponse();

            restResponse.CallInformations = new RESTCallInformations();
            restResponse.CallInformations.VERB = callModeServiceREST;

            if (parametres != null && parametres.Count > 0)
            {
                for (int i = 0; i < parametres.Count; i++)
                {
                    string type = parametres[i].RESTParameterType.ToString();
                    string idParam = parametres[i].IdParam;
                    string param = parametres[i].Param;

                    if (idParam == "Authorization" || idParam == "token")
                    {
                        param = "[VALEUR MASQUEE PAR SECURITE]";
                    }
                }
            }

            System.Net.ServicePointManager.Expect100Continue = false;

            restResponse.CallInformations.BaseURL = url;

            bool haveGETParameter = false;
            foreach (RESTParameter parametre in parametres)
            {
                switch (parametre.RESTParameterType)
                {
                    case RESTParameterType.GET:
                        if (!haveGETParameter)
                        {
                            url = url + "?" + parametre.IdParam + "=" + parametre.Param;
                            haveGETParameter = true;
                        }
                        else
                        {
                            url = url + "&" + parametre.IdParam + "=" + parametre.Param;
                        }

                        restResponse.CallInformations.BaseURL = url;
                        break;

                    case RESTParameterType.PATH:
                        url = url.Replace(parametre.IdParam, parametre.Param);
                        break;

                    case RESTParameterType.POST:
                    case RESTParameterType.HEADER:
                    default:
                        break;
                }
            }

            restResponse.CallInformations.CalledURL = url;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            restResponse.CallInformations.Domain = request.RequestUri.Scheme + "://" + request.RequestUri.Host;

            // Exploitation du Proxy de la machine   
            IWebProxy webProxy = WebRequest.DefaultWebProxy;
            webProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = webProxy;


            // Time-out
            request.Timeout = timeout;

            // Active la compression GZip et Deflate
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            foreach (RESTParameter headerRESTParameter in parametres)
            {
                if (headerRESTParameter.RESTParameterType == RESTParameterType.HEADER)
                {
                    request.Headers.Add(headerRESTParameter.IdParam, headerRESTParameter.Param);
                }
            }

            switch (callModeServiceREST)
            {
                case CallModeServiceREST.POST:
                case CallModeServiceREST.PUT:
                    RESTParameter postParameter = null;
                    foreach (RESTParameter postRESTParameter in parametres)
                    {
                        if (postRESTParameter.RESTParameterType == RESTParameterType.POST)
                        {
                            postParameter = postRESTParameter;
                        }
                    }

                    if (postParameter != null)
                    {
                        restResponse.CallInformations.Body = postParameter.Param;

                        byte[] data = Encoding.UTF8.GetBytes(postParameter.Param);

                        request.Method = callModeServiceREST.ToString().ToUpper();
                        request.ContentType = postContentType;
                        request.Accept = "application/json";

                        request.ContentLength = data.Length;

                        using (Stream stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                    break;

                case CallModeServiceREST.GET:
                    request.Method = "GET";
                    break;

                case CallModeServiceREST.DELETE:
                    request.Method = callModeServiceREST.ToString().ToUpper();
                    break;

                default:
                    throw new NotImplementedException();
            }

            restResponse.CallInformations.Headers = new Dictionary<string, string>();
            foreach (string headerKey in request.Headers.AllKeys)
            {
                restResponse.CallInformations.Headers.Add(headerKey, request.Headers[headerKey]);
            }

            DateTime debutAppel = DateTime.Now;
            DateTime finAppel = debutAppel.AddSeconds(-1);
            string retourAppel = string.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    retourAppel = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    restResponse.HTTPStatuCode = (int)response.StatusCode;
                    restResponse.Body = retourAppel;
                }
            }
            catch (Exception ex)
            {
                // default = UnknownError
                int httpErrorStatus = 16;
                string httpBody = string.Empty;
                int httpStatus = 0;
                if (ex is WebException)
                {
                    httpErrorStatus = (int)((WebException)ex).Status;
                    HttpWebResponse response = (HttpWebResponse)((WebException)ex).Response;

                    if (response != null)
                    {
                        httpBody = new StreamReader(((WebException)ex).Response.GetResponseStream()).ReadToEnd();
                        httpStatus = (int)response.StatusCode;
                    }
                }

                restResponse.HTTPStatuCode = httpStatus;
                restResponse.Body = httpBody;
                restResponse.Error = new RESTErrorDTO(RESTErrorDTO.RESTErrorType.Exception, httpErrorStatus, ex.Message);
            }
            finally
            {
                finAppel = DateTime.Now;

                restResponse.CallInformations.CallDuration = (finAppel - debutAppel).TotalMilliseconds;
            }

            return restResponse;
        }
    }
}