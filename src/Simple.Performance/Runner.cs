using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Simple.Performance
{
    public class Runner
    {
        int iterations;
        int top;
        IDictionary<string, TimeSpan> results = new Dictionary<string, TimeSpan>();
        public Runner(int iterations)
        {
            this.iterations = iterations;
            top = Console.CursorTop;
        }

        public void Execute(string name, Action<int> action)
        {
            var s = Stopwatch.StartNew();
            for (int i = 0; i < this.iterations; i++)
                action(i);
            results[name] = s.Elapsed;
            WriteAll(name);
        }

        public void WriteAll(string current)
        {
            Console.CursorTop = top;
            var ordered = results.OrderBy(x => x.Value).ToArray();

            var first = ordered.First().Value.TotalSeconds;
            foreach (var item in ordered)
            {
                Console.WriteLine(" {0}{1,-10}: {2} ({3:0.0}x)", (item.Key == current ? ">" : " "), item.Key, item.Value, item.Value.TotalSeconds / first);
            }
        }

        public void WriteHeader(string header)
        {
            Console.WriteLine("{0}", header);
            top++;
        }
    }
}
