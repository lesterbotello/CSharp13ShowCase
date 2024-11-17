// See https://aka.ms/new-console-template for more information

System.Console.WriteLine("Hello, World!");

// New in C# 9: params collection:
ListPerson(new Person("John", 30), new Person("Jane", 25));

void ListPerson<T>(params ReadOnlySpan<T> items) where T : Person
{
    foreach (var item in items)
    {
        Console.WriteLine($"{item.Name} is {item.Age} years old.");
    }
}

public record Person(string Name, int Age);