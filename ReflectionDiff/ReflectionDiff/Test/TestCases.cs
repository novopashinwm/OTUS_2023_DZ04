using Newtonsoft.Json;
using ReflectionDiff.CustomTools;
using ReflectionDiff.Data;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDiff.Test
{
    public static class TestCases
    {
        public static string SerializeCustomObject()
        {
            string delimeter = ";";

            var stopwatch = new Stopwatch();

            var objectList = new List<F>();

            for (int i = 0; i < 100000; i++)
                objectList.Add(new F());

            stopwatch.Start();

            var stringBuilder = new StringBuilder();

            var serializedToString = CustomSerializer.SerializeObjectList(objectList, delimeter);

            stringBuilder.Append(serializedToString);

            stopwatch.Stop();

            var serializedObject = stringBuilder.ToString();


            Console.WriteLine($"Время, затраченное на сериализацию CustomObject = {stopwatch.ElapsedMilliseconds}ms");

            return serializedObject;
        }

        public static void DeserializeCustomObject(string serializedObject)
        {
            string delimeter = ";";

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var deserializedListOfObjects = CustomSerializer.DeserializeObject<F>(serializedObject, delimeter);

            stopwatch.Stop();

            Console.WriteLine("Список десериализованных объектов содержит:\r");
            Console.WriteLine($"{deserializedListOfObjects!.Count} экземпляров");

            Console.WriteLine($"Время, затраченное на десериализацию CustomObject = {stopwatch.ElapsedMilliseconds}ms");
        }

        public static string SerializeObjectUsingStandardTools()
        {
            var stopwatch = new Stopwatch();

            var objectList = new List<F>();

            for (int i = 0; i < 100000; i++)
                objectList.Add(new F());
            

            stopwatch.Start();

            var serializedToString = JsonConvert.SerializeObject(objectList); // Использование NewtonSoft.Json

            stopwatch.Stop();

            Console.WriteLine($"Время, затраченное на сериализацию NewtonSoft.JSON = {stopwatch.ElapsedMilliseconds}ms");

            return serializedToString;
        }

        public static void DeserializeObjectUsingStandardTools(string serializedObject)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var deserializedListOfObjects = JsonConvert.DeserializeObject<List<F>>(serializedObject);

            stopwatch.Stop();

            Console.WriteLine($"Список десериализованных объектов содержит:");
            Console.WriteLine($"{deserializedListOfObjects!.Count} экземпляров");

            Console.WriteLine($"Время, затраченное на десериализацию NewtonSoft.JSON = {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
