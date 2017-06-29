using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreepsGUI.Tools;
using ScreepsGUI.Tools.Json.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScreepsGUI.Tools.Json.Converters
{
    /// <summary>
    /// Le JsonPathConverter permet, avec les JsonPathAttribute, de lancer une recherche dans le Json à partir d'un ou plusieurs XPath.
    /// </summary>
    public class JsonPathConverter : JsonConverter
    {
        /// <summary>
        /// Méthode de lecture du Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);

            foreach (PropertyInfo prop in objectType.GetProperties())
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    JsonPropertyAttribute propAtt = null;
                    JsonPathAttribute pathAtt = null;
                    JsonConverterAttribute convAtt = null;
                    List<string> jsonPaths = null;

                    ToolsJsonNet.GetJsonAttributeAndPathsByMemberInfo(prop, out propAtt, out pathAtt, out convAtt, out jsonPaths);

                    #region Application du Converter

                    JsonConverter conv = null;
                    if (serializer != null && convAtt != null)
                    {
                        conv = (JsonConverter)Activator.CreateInstance(convAtt.ConverterType);

                        serializer.Converters.Add(conv);
                    }

                    #endregion

                    JToken token = null;
                    foreach (string jsonPath in jsonPaths)
                    {
                        token = jo.SelectToken(jsonPath);

                        // On exploite le 1er path qui match avec notre Json
                        if (token != null) { break; }
                    }

                    if (token != null && token.Type != JTokenType.Null)
                    {
                        // Si le token est un noeud final de l'arbre, qu'il n'est pas une chaîne de caractére et que le noeud Json ne posséde pas de valeur
                        if (token is JValue && prop.PropertyType != typeof(string) && ((JValue)token).Value is string && ((string)((JValue)token).Value) == string.Empty)
                        {
                            // On assigne la valeur par default du Type (cela évite des probléme de cast avec les int et autre type)
                            prop.SetValue(targetObj, ToolsType.GetDefault(prop.PropertyType), null);
                        }
                        else
                        {
                            object value = token.ToObject(prop.PropertyType, serializer);
                            prop.SetValue(targetObj, value, null);
                        }
                    }
                }
            }

            foreach (FieldInfo field in objectType.GetFields())
            {
                JsonPropertyAttribute propAtt = null;
                JsonPathAttribute pathAtt = null;
                JsonConverterAttribute convAtt = null;
                List<string> jsonPaths = null;

                ToolsJsonNet.GetJsonAttributeAndPathsByMemberInfo(field, out propAtt, out pathAtt, out convAtt, out jsonPaths);

                #region Application du Converter

                JsonConverter conv = null;
                if (serializer != null && convAtt != null)
                {
                    conv = (JsonConverter)Activator.CreateInstance(convAtt.ConverterType);

                    serializer.Converters.Add(conv);
                }

                #endregion

                JToken token = null;
                foreach (string jsonPath in jsonPaths)
                {
                    token = jo.SelectToken(jsonPath);

                    // On exploite le 1er path qui match avec notre Json
                    if (token != null) { break; }
                }

                if (token != null && token.Type != JTokenType.Null)
                {
                    // Si le token est un noeud final de l'arbre, qu'il n'est pas une chaîne de caractére et que le noeud Json ne posséde pas de valeur
                    if (token is JValue && field.FieldType != typeof(string) && ((JValue)token).Value is string && ((string)((JValue)token).Value) == string.Empty)
                    {
                        // On assigne la valeur par default du Type (cela évite des probléme de cast avec les int et autre type)
                        field.SetValue(targetObj, ToolsType.GetDefault(field.FieldType));
                    }
                    else
                    {
                        object value = token.ToObject(field.FieldType, serializer);
                        field.SetValue(targetObj, value);
                    }
                }
            }

            return targetObj;
        }

        /// <summary>
        /// CanConvert is not called when [JsonConverter] attribute is used
        /// </summary>
        /// <param name="objectType">not useed</param>
        /// <returns>always false</returns>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        /// <summary>
        /// JsonPathConverter ne permet pas l'écriture (car chemin multiple)
        /// </summary>
        /// <returns>Toujours false</returns>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// JsonPathConverter ne permet pas l'écriture (car chemin multiple).
        /// Léve une NotImplementedException
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}