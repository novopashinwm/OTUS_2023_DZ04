using ReflectionDiff.Test;


Console.WriteLine("Домашнее задание: рефлексия.");
Console.WriteLine();

Console.WriteLine("Демонстрация custom сериализации/десериализации (в CSV) с использование рефлексии");
Console.WriteLine();

var serializedObject = TestCases.SerializeCustomObject();

TestCases.DeserializeCustomObject(serializedObject);

Console.WriteLine("Демонстрация сериализации/десериализации с использованием стандартных средств");
Console.WriteLine();

var serializedObject2 = TestCases.SerializeObjectUsingStandardTools();

TestCases.DeserializeObjectUsingStandardTools(serializedObject2);

Console.WriteLine("Тест завершен");
Console.ReadLine();