using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.DynamicProxy;
using Simple.Reflection;

namespace Simple.Tests.DynamicProxy
{
    [TestFixture]
    public class MarshallableDynamicProxyFixture
    {
        public class SimpleServer : MarshalByRefObject
        {
            public int GetInt() { return 10; }
            public string GetString() { return "4"; }
            public void TestVoid(int param) { }
            public int TestIntParamReturn(int param) { return param; }
            public T TestGenericParamReturn<T>(T param)
            {
                return param;
            }
            public void TestRefAndOut(ref int p1, out int p2) { p2 = p1; p1 = 42; }
            public void TestException() { throw new InvalidCastException(); }
        }

        [Test]
        public void TestIntReturning()
        {
            SimpleServer server = new SimpleServer();
            server.GetInt().Should().Be(10);

            server = (SimpleServer)new EasyProxy((o, m, p) => 42).CreateMarshallable(server);
            server.GetInt().Should().Be(42);
        }

        [Test]
        public void TestStringReturning()
        {
            SimpleServer server = new SimpleServer();
            server.GetString().Should().Be("4");

            server = (SimpleServer)new EasyProxy((o, m, p) => m.Invoke(o, p) + "2").CreateMarshallable(server);
            server.GetString().Should().Be("42");
        }

        [Test]
        public void TestVoidReturning()
        {
            SimpleServer server = new SimpleServer();
            server.TestVoid(10);

            server = (SimpleServer)new EasyProxy((o, m, p) => m.Invoke(o, p)).CreateMarshallable(server);
            server.TestVoid(10);
        }

        [Test]
        public void TestIntParamReturning()
        {
            SimpleServer server = new SimpleServer();
            server.TestIntParamReturn(10).Should().Be(10);

            server = (SimpleServer)new EasyProxy((o, m, p) => (int)m.Invoke(o, p) + 1).CreateMarshallable(server);
            server.TestIntParamReturn(10).Should().Be(11);
        }

        [Test]
        public void TestGenericParamReturning()
        {
            SimpleServer server = new SimpleServer();
            server.TestGenericParamReturn(10).Should().Be(10);

            server = (SimpleServer)new EasyProxy((o, m, p) => m.Invoke(o, p) + "2").CreateMarshallable(server);
            server.TestGenericParamReturn("4").Should().Be("42");
        }

        [Test, ExpectedException(typeof(InvalidCastException))]
        public void TestException()
        {
            SimpleServer server = new SimpleServer();

            server = (SimpleServer)new EasyProxy((o, m, p) =>
                {
                    var invoker = new DelegateFactory().Create(m);
                    invoker.Invoke(o, p);
                    return null;
                }).CreateMarshallable(server);

            server.TestException();
        }


        [Test]
        public void TestRefAndOut()
        {
            SimpleServer server = new SimpleServer();
            int a = 10; int b;

            server.TestRefAndOut(ref a, out b);
            a.Should().Be(42);
            b.Should().Be(10);

            server = (SimpleServer)new EasyProxy(
                (o, m, p) => { p[0] = 41; p[1] = 42; return null; }).CreateMarshallable(server);

            server.TestRefAndOut(ref a, out b);
            a.Should().Be(41);
            b.Should().Be(42);
        }


    }
}
