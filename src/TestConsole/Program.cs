using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using System.Diagnostics;

namespace TestConsole
{
    class Program
    {
        public Program(int a, string b, DateTime c) { }

        static void Main(string[] args)
        {
            var methods = new MethodCache();
            var method = typeof(Program).GetConstructor(new[] { typeof(int), typeof(string), typeof(DateTime) });
            var method2 = typeof(Program).GetConstructor(new[] { typeof(int), typeof(string), typeof(DateTime) });
            var invoker1 = methods.GetInvoker(method);

            var s = Stopwatch.StartNew();

            int iter = 1000000;

            s.Reset(); s.Start();
            for (int i = 0; i < iter; i++)
                new Program(2, "a", DateTime.MinValue);
            Console.WriteLine(s.Elapsed);

            s.Reset(); s.Start();
            for (int i = 0; i < iter; i++)
                invoker1(null, 2, "a", DateTime.MinValue);
            Console.WriteLine(s.Elapsed);

            s.Reset(); s.Start();
            for (int i = 0; i < iter; i++)
                methods.GetInvoker(method)(null, 2, "a", DateTime.MinValue);
            Console.WriteLine(s.Elapsed);

            s.Reset(); s.Start();
            for (int i = 0; i < iter; i++)
                methods.CreateInstance<Program>(2, "a", DateTime.MinValue);
            Console.WriteLine(s.Elapsed);

            s.Reset(); s.Start();
            for (int i = 0; i < iter; i++)
                Activator.CreateInstance(typeof(Program), 2, "a", DateTime.MinValue);
            Console.WriteLine(s.Elapsed);

            Console.ReadLine();
        }
    }
}
