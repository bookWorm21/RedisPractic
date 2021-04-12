using StackExchange.Redis;
using System;
using System.Threading;

namespace Sender
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
            int firstNumber = 0;
            int secondNumber = 0;
            int betweenTime = 300;

            while (userCommand != "exit")
            {
                Console.WriteLine("Input command");
                Console.WriteLine("-input numbers");
                Console.WriteLine("-exit");
                userCommand = Console.ReadLine();

                if (userCommand == "input numbers")
                {
                    Console.WriteLine("Input first number");
                    if (int.TryParse(Console.ReadLine(), out firstNumber))
                    {
                        Console.WriteLine("Input second number");
                        if (int.TryParse(Console.ReadLine(), out secondNumber))
                        {
                            db.StringSet(firstValueKey, firstNumber);
                            db.StringSet(secondValueKey, secondNumber);
                            Console.WriteLine("Succesful writing numbers");
                        }
                        else
                        {
                            Console.WriteLine("Uncorrect value");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Uncorrect value");
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
