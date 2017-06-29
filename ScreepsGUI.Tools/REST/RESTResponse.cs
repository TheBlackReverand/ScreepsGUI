namespace ScreepsGUI.Tools.REST
{
    public class RESTResponse
    {
        internal RESTResponse() { }

        /// <summary>
        /// HTTP Statu Code
        /// </summary>
        public int HTTPStatuCode { get; internal set; }

        private string body;
        /// <summary>
        /// Body de la réponse HTTP
        /// </summary>
        public string Body
        {
            get { return body; }
            internal set
            {
                body = value;
                RESTErrorDTO.CheckBodyIfHaveError(body);
            }
        }

        /// <summary>
        /// Information sur l'exception rencontrée
        /// null si pas d'exception
        /// </summary>
        public RESTErrorDTO Error { get; internal set; }

        /// <summary>
        /// Information sur les éléments qui ont était utilisé pour lancé l'appel
        /// </summary>
        public RESTCallInformations CallInformations { get; internal set; }
    }
}
