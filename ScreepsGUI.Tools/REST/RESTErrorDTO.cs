using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace ScreepsGUI.Tools.REST
{
    [JsonObject(MemberSerialization.Fields)]
    public class RESTErrorDTO
    {
        public enum RESTErrorType
        {
            Exception,
            InBody,
        }

        internal RESTErrorDTO(RESTErrorType errorType, int errorCode, string errorMessage)
        {
            ErrorType = errorType;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Type d'erreur : Exception, InBody
        /// </summary>
        public RESTErrorType ErrorType { get; internal set; }

        /// <summary>
        /// System.Net.WebExceptionStatus si identifiable, sinon 16
        /// </summary>
        public int ErrorCode { get; internal set; }

        /// <summary>
        /// Message de l'exception rencontrée
        /// </summary>
        public string ErrorMessage { get; internal set; }

        /// <summary>
        /// Certaines réponses (body) peuvent contenir une erreur et pourtant renvoyer un code http 200
        /// </summary>
        /// <param name="retourWS"></param>
        /// <returns></returns>
        public static RESTErrorDTO CheckBodyIfHaveError(Newtonsoft.Json.Linq.JToken retourWS)
        {
            JToken jtError, jtErrorCode, jtErrorMessage, jtHttpBody;

            jtError = retourWS.SelectToken("error");
            jtErrorCode = retourWS.SelectToken("ErrorCode");
            jtErrorMessage = retourWS.SelectToken("ErrorMessage");
            jtHttpBody = retourWS.SelectToken("HttpBody");

            if (jtErrorCode != null || jtErrorMessage != null || jtHttpBody != null)
            {
                int code = 0;
                string message = string.Empty;

                if (jtError != null)
                {
                    // message = ToolsJsonNet.GetValue<string>(jtError);
                    throw new Exception("check jtError");
                }
                if (jtErrorCode != null)
                {
                    // code = ToolsJsonNet.GetValue<int>(jtErrorCode);
                    throw new Exception("check jtErrorCode");
                }
                if (jtErrorMessage != null)
                {
                    // message = ToolsJsonNet.GetValue<string>(jtErrorMessage).Trim();
                    throw new Exception("check jtErrorMessage");
                }
                if (jtHttpBody != null)
                {
                    throw new Exception("check jtHttpBody");
                    /*
                    if (!String.IsNullOrEmpty(message) && ToolsJsonNet.GetValue<string>(jtHttpBody).Trim().Length > 0)
                    {
                        message += Environment.NewLine;
                    }

                    message += ToolsJsonNet.GetValue<string>(jtHttpBody).Replace("{", string.Empty)
                                                            .Replace("}", string.Empty)
                                                            .Replace("\\\"", string.Empty)
                                                            .Replace("\"", string.Empty)
                                                            .Replace("  ", " ").Replace("  ", " ")
                                                            .Trim();
                     */
                }

                return new RESTErrorDTO(RESTErrorType.InBody, code, message);
            }
            else
            {
                return null;
            }
        }
    }
}