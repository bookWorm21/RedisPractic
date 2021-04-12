using StackExchange.Redis;
using System;
using System.Threading;

namespace Source
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("It is practic for using redis");

            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localHost", 6379);

            var redis = ConnectionMultiplexer.Connect(configuration);
            var db = redis.GetDatabase(0);

            const string firstValueKey = "first";
            const string secondValueKey = "second";

            string userCommand = "";
            int betweenTime = 500;

            while (userCommand != "exit")
            {
                Console.WriteLine("Input command");
                Console.WriteLine("-calculate composition");
                Console.WriteLine("-exit");
                userCommand = Console.ReadLine();

                if (userCommand == "calculate composition")
                {
                    if (db.KeyExists(firstValueKey) || db.KeyExists(secondValueKey))
                    {
                        Console.WriteLine($"composition - {(int)db.StringGet(firstValueKey) * (int)db.StringGet(secondValueKey)}");
                    }
                    else
                    {
                        Console.WriteLine("not all numbers");
                    }
                }
                else if (userCommand != "exit")
                {
                    Console.WriteLine("Uncorrect command");
                }

                Thread.Sleep(betweenTime);
                Console.Clear();
            }
        }
    }
}
