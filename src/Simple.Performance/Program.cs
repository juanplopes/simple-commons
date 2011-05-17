using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Performance.Fixtures;

namespace Simple.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute<PropertyWrapper>();

            Console.ReadLine();
        }

        static void Execute<T>(int iterations = 1000000)
            where T : IFixture, new()
        {
            var runner = new Runner(iterations);
            var fixture = new T();
            runner.WriteHeader(typeof(T).Name);
            fixture.Execute(runner);
        }
    }
}
