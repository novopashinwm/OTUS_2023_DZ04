using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDiff.CustomTools
{
    public static class CustomSerializer
    {
        public static string SerializeObject<T>(T? objectToSerialize, string delimeter)
        {
            if (objectToSerialize == null)
                throw new ArgumentNullException("Передаваемы объект не должен быть NULL");

            var name = objectToSerialize.GetType().Name;
            Type? type = objectToSerialize.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic 
                | BindingFlags.Public | BindingFlags.Static);

            var sb = new StringBuilder();
            sb.Append($"{"name"}{delimeter}");

            foreach (var field in fields)
                sb.Append($"{field.Name}{delimeter}");

            sb.AppendLine();

            sb.Append($"{name}{delimeter}");

            foreach (var field in fields)
                sb.Append($"{field.GetValue(objectToSerialize)!.ToString()}{delimeter}");

            sb.AppendLine();
            return sb.ToString();
        }

        public static string SerializeObjectList<T>(List<T>? objectToSerializeList, string delimeter)
        {
            if (objectToSerializeList == null)
                throw new ArgumentNullException("Передаваемы объект не должен быть NULL");

            var name = objectToSerializeList[0]!.GetType().Name;
            Type? type = objectToSerializeList[0]!.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            var sb = new StringBuilder();
            sb.Append($"{"name"}{delimeter}");

            foreach (var field in fields)
                sb.Append($"{field.Name}{delimeter}");

            sb.AppendLine();

            foreach (var item in objectToSerializeList)
            {
                sb.Append($"{item!.GetType().Name}{delimeter}");

                foreach (var field in fields)                
                    sb.Append($"{field.GetValue(item)!.ToString()}{delimeter}");
                                    
                sb.AppendLine();
            }

            sb.AppendLine();

            return sb.ToString();
        }

        public static List<T> DeserializeObject<T>(string serializedObjectByCsv, string delimeter)
        {
            if (string.IsNullOrEmpty(serializedObjectByCsv))
                throw new ArgumentNullException("Проверьте сериализованную строку. Строка не должна быть пустой");


            var type = typeof(T);

            var objectName = type.Name;

            var sliptSerializedObjectArray = serializedObjectByCsv.Split(Environment.NewLine);

            var splitFildsNamesArray = sliptSerializedObjectArray[0].Split(";");

            var listF = new List<T>();

            if (sliptSerializedObjectArray.Length > 1)
            {
                for (int i = 1; i < sliptSerializedObjectArray.Length - 1; i++)
                {
                    var splitValuesArray = sliptSerializedObjectArray[i].Split(";");

                    if (objectName == splitValuesArray[0])
                    {
                        var inctance = (T)Activator.CreateInstance(typeof(T))!;

                        for (int j = 1; j < splitValuesArray.Length - 1; j++)
                        {
                            var name = type.GetField(splitFildsNamesArray[j], BindingFlags.Instance | BindingFlags.NonPublic);
                            name?.SetValue(inctance, int.Parse(splitValuesArray[j]));
                        }

                        listF.Add(inctance);
                    }
                }
            }

            return listF;
        }
    }
}
