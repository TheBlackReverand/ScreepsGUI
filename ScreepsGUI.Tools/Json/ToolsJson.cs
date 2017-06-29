using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScreepsGUI.Tools.Json.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScreepsGUI.Tools.Json
{
    public static class ToolsJsonNet
    {
#if !NET2
        public static T GetValue<T>(this Newtonsoft.Json.Linq.JToken jtoken, string path)
        {
            return jtoken.SelectToken(path).GetValue<T>();
        }
#else
        public static T GetValue<T>(Newtonsoft.Json.Linq.JToken jtoken, string path)
        {
            return GetValue<T>(jtoken.SelectToken(path));
        }
#endif

#if !NET2
        public static T GetValue<T>(this Newtonsoft.Json.Linq.JToken jtoken)
#else
        public static T GetValue<T>(Newtonsoft.Json.Linq.JToken jtoken)
#endif
        {
            switch (jtoken.Type)
            {
                case Newtonsoft.Json.Linq.JTokenType.Null:
                    return default(T);


                case Newtonsoft.Json.Linq.JTokenType.Integer:
                    if (typeof(T) == typeof(int))
                    {
                        return (T)(object)(int)jtoken;
                    }
                    break;
                case Newtonsoft.Json.Linq.JTokenType.String:
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)(string)jtoken;
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        return (T)(object)(int)jtoken;
                    }
                    break;


                case Newtonsoft.Json.Linq.JTokenType.Array:
                case Newtonsoft.Json.Linq.JTokenType.Boolean:
                case Newtonsoft.Json.Linq.JTokenType.Bytes:
                case Newtonsoft.Json.Linq.JTokenType.Comment:
                case Newtonsoft.Json.Linq.JTokenType.Constructor:
                case Newtonsoft.Json.Linq.JTokenType.Date:
                case Newtonsoft.Json.Linq.JTokenType.Float:
                case Newtonsoft.Json.Linq.JTokenType.Guid:
                case Newtonsoft.Json.Linq.JTokenType.None:
                case Newtonsoft.Json.Linq.JTokenType.Object:
                case Newtonsoft.Json.Linq.JTokenType.Property:
                case Newtonsoft.Json.Linq.JTokenType.Raw:
                case Newtonsoft.Json.Linq.JTokenType.TimeSpan:
                case Newtonsoft.Json.Linq.JTokenType.Undefined:
                case Newtonsoft.Json.Linq.JTokenType.Uri:
                default:
                    break;
            }

            throw new NotSupportedException("ToolsJsonNet.GetValue<T>() : jtoken.Type = " + jtoken.Type.ToString() + " -- typeof(T).Name = " + typeof(T).Name);
        }

        public static void PopulateStruct<T>(Newtonsoft.Json.Linq.JToken jsonToken, ref T target)
        {
            target = Populate<T>(jsonToken, target, null);
        }
        public static void PopulateStruct<T>(Newtonsoft.Json.Linq.JToken jsonToken, ref T target, JsonSerializer serializer)
        {
            target = Populate<T>(jsonToken, target, serializer);
        }

        public static void PopulateClass<T>(Newtonsoft.Json.Linq.JToken jsonToken, T target)
        {
            target = Populate<T>(jsonToken, target, null);
        }
        public static void PopulateClass<T>(Newtonsoft.Json.Linq.JToken jsonToken, T target, JsonSerializer serializer)
        {
            target = Populate<T>(jsonToken, target, serializer);
        }

        private static T Populate<T>(Newtonsoft.Json.Linq.JToken jsonToken, T target, JsonSerializer serializer)
        {
            // Si on exploite "target" sous sa forme typée alors le SetValue n'impacte pas l'instance de notre objet
            object targetObject = (object)target;

            Type targetType = target.GetType();

            Dictionary<List<string>, MemberInfo> lstMembers = new Dictionary<List<string>, MemberInfo>();

            List<MemberInfo> memberInfosAtCheck = new List<MemberInfo>();
            memberInfosAtCheck.AddRange(targetType.GetProperties());
            memberInfosAtCheck.AddRange(targetType.GetFields());

            foreach (MemberInfo memberInfo in memberInfosAtCheck)
            {
                JsonPropertyAttribute propAtt = null;
                JsonConverterAttribute convAtt = null;
                JsonPathAttribute pathAtt = null;
                List<string> jsonPaths = null;

                ToolsJsonNet.GetJsonAttributeAndPathsByMemberInfo(memberInfo, out propAtt, out pathAtt, out convAtt, out jsonPaths);

                #region Application du Converter

                JsonConverter conv = null;
                if (serializer != null && convAtt != null)
                {
                    conv = (JsonConverter)Activator.CreateInstance(convAtt.ConverterType);

                    serializer.Converters.Add(conv);
                }

                #endregion

                lstMembers.Add(jsonPaths, memberInfo);
            }

            foreach (List<string> memberPaths in lstMembers.Keys)
            {
                JToken jToken = null;
                foreach (string memberPath in memberPaths)
                {
                    IEnumerator<JToken> jTokens = jsonToken.SelectTokens(memberPath).GetEnumerator();

                    // Récupération du 1er element de la liste (si existant)
                    if (jTokens.MoveNext())
                    {
                        jToken = jTokens.Current;
                    }

                    // On exploite le 1er path qui match avec notre Json
                    if (jToken != null) { break; }
                }

                if (jToken != null && jToken.Type != JTokenType.Null)
                {
                    if (lstMembers[memberPaths] is PropertyInfo)
                    {
                        PropertyInfo propertyInfo = (PropertyInfo)lstMembers[memberPaths];

                        object value = ExtractValue(serializer, jToken, propertyInfo.PropertyType);

                        propertyInfo.SetValue(targetObject, value, null);
                    }
                    else
                    {
                        FieldInfo fieldInfo = (FieldInfo)lstMembers[memberPaths];

                        object value = ExtractValue(serializer, jToken, fieldInfo.FieldType);

                        fieldInfo.SetValue(targetObject, value);
                    }
                }
            }

            return (T)targetObject;
        }


        private static object ExtractValue(JsonSerializer serializer, JToken jToken, Type memberInfoType)
        {
            if (serializer == null)
            {
                return jToken.ToObject(memberInfoType);
            }
            else
            {
                return jToken.ToObject(memberInfoType, serializer);
            }
        }

        internal static void GetJsonAttributeAndPathsByMemberInfo(MemberInfo memberInfo, out JsonPropertyAttribute propAtt, out JsonPathAttribute pathAtt, out JsonConverterAttribute convAtt, out List<string> jsonPaths)
        {
            propAtt = null;
            pathAtt = null;
            convAtt = null;

            jsonPaths = new List<string>();

            foreach (object customAttribute in memberInfo.GetCustomAttributes(true))
            {
                if (customAttribute.GetType() == typeof(JsonPropertyAttribute))
                {
                    propAtt = customAttribute as JsonPropertyAttribute;
                }
                else if (customAttribute.GetType() == typeof(JsonPathAttribute))
                {
                    pathAtt = customAttribute as JsonPathAttribute;
                }
                else if (customAttribute.GetType() == typeof(JsonConverterAttribute))
                {
                    convAtt = customAttribute as JsonConverterAttribute;
                }
            }

            #region Récupération des Path par priorité : JsonPath / JsonProperty / PropertyName

            if (propAtt != null && !String.IsNullOrEmpty(propAtt.PropertyName))
            {
                jsonPaths.Add(propAtt.PropertyName);
            }

            if (pathAtt != null)
            {
                foreach (string path in pathAtt.JPaths)
                {
                    jsonPaths.Add(path);
                }
            }

            jsonPaths.Add(memberInfo.Name);

            #endregion
        }

        public static List<string> EnumerateFieldsPath(JToken jsonToken)
        {
            List<string> lstFields = new List<string>();

            // DeepClone créé une copie du JToken sans prendre en compte les noeuds précédents, ainsi l'objet json courant deviens le Root
            // Cela permet d'obtenir un Path propre et sans ancétres
            JToken jsonTokenWithNoAncestors = jsonToken.DeepClone();

            foreach (JToken jToken in jsonTokenWithNoAncestors)
            {
                lstFields.Add(jToken.Path);
            }

            return lstFields;
        }
    }
}