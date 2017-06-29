namespace ScreepsGUI.Tools.REST
{
    public class RESTParameter
    {
        public RESTParameter(RESTParameterType RESTParameterType, string param)
        {
            this.RESTParameterType = RESTParameterType;
            this.Param = param;
        }
        public RESTParameter(RESTParameterType RESTParameterType, string idParam, string param)
            : this(RESTParameterType, param)
        {
            this.IdParam = idParam;
        }

        public RESTParameterType RESTParameterType { get; set; }

        public string IdParam { get; set; }
        public string Param { get; set; }
    }
}