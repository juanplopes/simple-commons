using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;

namespace Simple.Performance.Fixtures
{
    public class PropertyWrapper : IFixture
    {
        class TestClass
        {
            public string TestProperty { get; set; }
        }

        public void Execute(Runner my)
        {
            var prop = typeof(TestClass).GetProperty("TestProperty");
            var obj = new TestClass();

            my.Execute("raw", i =>
            {
                obj.TestProperty = "asd";
            });

            var stdDel = (Action<TestClass, string>)Delegate.CreateDelegate(typeof(Action<TestClass, string>), prop.GetSetMethod());
            my.Execute("stddel", i =>
            {
                stdDel(obj, "asd");
            });

            var genDel = new MethodCache().GetSetter(prop);
            my.Execute("gendel", i =>
            {
                genDel(obj, "asd");
            });

            var cache = new MethodCache();
            var method = prop.GetSetMethod();
            cache.GetInvoker(method);
            my.Execute("gendelc", i =>
            {
                cache.GetInvoker(method)(obj, "asd");
            });

            my.Execute("gendelc2", i =>
            {
                cache.GetSetter(prop)(obj, "asd");
            });

            my.Execute("invoke", i =>
            {
                method.Invoke(obj, new[] { "asd" });
            });

            my.Execute("setvalue", i =>
            {
                prop.SetValue(obj, "asd", null);
            });

            var settable = prop.ToSettable();
            my.Execute("settable", i =>
            {
                settable.Set(obj, "asd");
            });
        }
    }
}
