namespace ScreepsGUI.Tools.REST
{
    public enum RESTParameterType
    {
        /// <summary>
        /// Paramétre ajouté en fin d'URL après un ?
        /// </summary>
        GET,

        /// <summary>
        /// Paramétre ajouté en POST via le body de la requête REST
        /// </summary>
        POST,

        /// <summary>
        /// Paramétre transmit en Header
        /// </summary>
        HEADER,

        /// <summary>
        /// Paramétre à appliquer via un replace dans l'URL
        /// Exemple : http://dpicmoappsvc10:32769/carriages/{carriageId}/bills
        /// </summary>
        PATH,
    }
}