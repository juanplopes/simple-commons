using System;
using System.Reflection;

namespace Simple.DynamicProxy
{
    public delegate object InvocationDelegate(object target, MethodBase method, object[] parameters);

    public interface IDynamicProxy
    {
        object ProxyTarget { get; }
        InvocationDelegate InvocationHandler { get; }
        object GetTransparentProxy();
       
    }
}
