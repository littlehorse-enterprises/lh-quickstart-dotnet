using LittleHorse.Sdk.Worker;

namespace QuickStart.Dotnet
{
    public class GreetWorker
    {
        [LHTaskMethod("greet")]
        public string Greeting(string name)
        {
            var message = $"Hello team, This is a Dotnet Worker";
            Console.WriteLine($"Executing task greet {name}: " + message);
            return message;
        }
    }
}