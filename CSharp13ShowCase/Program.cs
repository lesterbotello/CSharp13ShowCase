#region Params Collection
// New in C# 13: params collection:
// ****************************************************************************
// ListPerson(new Person("John", 30), new Person("Jane", 25));
// return;
//
// void ListPerson<T>(params ReadOnlySpan<T> items) where T : Person
// {
//     foreach (var item in items)
//     {
//         Console.WriteLine($"{item.Name} is {item.Age} years old.");
//     }
// }
// ****************************************************************************
#endregion

#region Lock Object
// New in C# 13: System.Threading.Lock
// Shared counter
// Old way:
// ****************************************************************************
// var sharedCounter = 0;
// var lockObject = new object();
// Console.WriteLine("Starting threads...");
// var thread1 = new Thread(IncrementCounter);
// var thread2 = new Thread(IncrementCounter);
// var thread3 = new Thread(IncrementCounter);
//
// thread1.Start();
// thread2.Start();
// thread3.Start();
//
// // Wait for all threads to finish
// thread1.Join();
// thread2.Join();
// thread3.Join();
//
// Console.WriteLine($"Final counter value: {sharedCounter}");
// return;
//
// void IncrementCounter()
// {
//     for (var i = 0; i < 1000; i++)
//     {
//         // Lock the shared resource to prevent race conditions
//         lock (lockObject)
//         {
//             sharedCounter++;
//         }
//     }
// }
// ****************************************************************************

// New way #1:
// ****************************************************************************
// var sharedCounter = 0;
// Lock lockObject = new();
// Console.WriteLine("Starting threads...");
// var thread1 = new Thread(IncrementCounter);
// var thread2 = new Thread(IncrementCounter);
// var thread3 = new Thread(IncrementCounter);
//
// thread1.Start();
// thread2.Start();
// thread3.Start();
//
// // Wait for all threads to finish
// thread1.Join();
// thread2.Join();
// thread3.Join();
//
// Console.WriteLine($"Final counter value: {sharedCounter}");
// return;
//
// void IncrementCounter()
// {
//     for (var i = 0; i < 1000; i++)
//     {
//         // Lock the shared resource to prevent race conditions
//         lock (lockObject)
//         {
//             sharedCounter++;
//         }
//     }
// }
// ****************************************************************************

// New way #2:
// ****************************************************************************
// Lock lockObject = new();
// // You can also do this, but you shouldn't:
// // object lockObject = new Lock();
// var sharedCounter = 0;
// var tasks = new Task[5];
// for (var i = 0; i < tasks.Length; i++)
// {
//     tasks[i] = Task.Run(IncrementCounter); // More about method groups later :)
// }
// await Task.WhenAll(tasks);
// Console.WriteLine($"Final counter value: {sharedCounter}");
// return;
//
// void IncrementCounter()
// {
//     // Automatically manages scope using System.Threading.Lock
//     lock (lockObject)
//     {
//         var temp = sharedCounter;
//         temp++; // Simulate some work
//         Task.Delay(100).Wait(); // Simulated delay
//         sharedCounter = temp;
//         Console.WriteLine($"Counter incremented to {sharedCounter}");
//     }
// }
// ****************************************************************************
#endregion

#region New Escape Sequence
// New in C# 13: New escape sequence "\e"
// ****************************************************************************
// var ansiEscapeSequence = "\e[31mThis text is red!\e[0m";
// Console.WriteLine(ansiEscapeSequence);
// var unicodeLongEscapeSequence = "\u001b[31mThis text is also red!\u001b[0m";
// var unicodeShortEscapeSequence = "\x1b[31mThis text is also red!\x1b[0m";
// Console.WriteLine(unicodeLongEscapeSequence);
// Console.WriteLine(unicodeShortEscapeSequence);
// Console.WriteLine($"Are they equivalent? {ansiEscapeSequence == unicodeLongEscapeSequence}");
#endregion

#region field keyword
// New in C# 13: field keyword
// ****************************************************************************
// public class User
// {
//     // Old way:
//     // ****************************************************************************
//     private string _name;
//     public string Name
//     {
//         get => _name;
//         set => _name = value;
//     }
//     // ****************************************************************************
//     // New way:
//     // Requires adding "<LangVersion>preview</LangVersion>" to the .csproj file
//     // ****************************************************************************
//     // public string Name
//     // {
//     //     get => field;
//     //     set => field = value;
//     // }
//     // ****************************************************************************
// }
#endregion

#region ref struct interfaces y allows ref struct
int[] numbers = [1, 2, 3, 4, 5];
var span = new Span<int>(numbers);

var example = new RefStructExample(span);
example.Process();

Console.WriteLine(string.Join(", ", numbers)); // Output: 2, 4, 6, 8, 10
public ref struct RefStructExample(Span<int> data) : IDisposable
{
    private Span<int> _data = data;

    public void Process()
    {
        for (var i = 0; i < _data.Length; i++)
        {
            _data[i] *= 2; // Double each element
        }
    }

    public void Dispose()
    {
        _data = null;
    }
}
#endregion

#region Overload resolution priority
// using System.Collections.Immutable;
//
// Disposer.DisposeAll<StringReader>(new StringReader("Hello"), new StringReader("World"));
//
// public static class Disposer
// {
//     public static void DisposeAll<T>(params IEnumerable<T> disposables) where T : IDisposable
//     {
//         foreach (var disposable in disposables)
//         {
//             disposable.Dispose();
//         }
//     }
//     
//     public static void DisposeAll<T>(params ReadOnlySpan<T> disposables) where T : IDisposable
//     {
//         foreach (var disposable in disposables)
//         {
//             disposable.Dispose();
//         }
//     }
//     
//     public static void DisposeAll<T>(params ImmutableArray<T> disposables) where T : IDisposable
//     {
//         foreach (var disposable in disposables)
//         {
//             disposable.Dispose();
//         }
//     }
// }
#endregion