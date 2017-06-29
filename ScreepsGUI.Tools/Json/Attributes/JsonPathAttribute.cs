using System;

namespace ScreepsGUI.Tools.Json.Attributes
{
    /// <summary>
    /// Le JsonPathAttribute permet aux JsonPathConverter et aux JsonStructAttribute de lancer une recherche dans le Json à partir d'un ou plusieurs XPath.
    /// Le 1er XPath de la liste avec une correspondance sera choisi
    /// </summary>
    public class JsonPathAttribute : Attribute
    {
        /// <summary>
        /// Liste des XPath à appliquer au Json
        /// </summary>
        public string[] JPaths { get; private set; }

        /// <summary>
        /// Constructeur standard
        /// </summary>
        /// <param name="jPaths">Liste des XPath à appliquer au Json</param>
        public JsonPathAttribute(params string[] jPaths)
        {
            JPaths = jPaths;
        }
    }
}