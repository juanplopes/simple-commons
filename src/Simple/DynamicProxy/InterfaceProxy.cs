using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;

namespace Simple.DynamicProxy
{
    public class InterfaceProxy : RealProxy, IDynamicProxy, IRemotingTypeInfo
    {
        public object ProxyTarget { get { return proxyTarget; } }
        public InvocationDelegate InvocationHandler { get { return invocationHandler; } }

        private object proxyTarget;
        private InvocationDelegate invocationHandler;

        protected internal InterfaceProxy(object proxyTarget, InvocationDelegate invocationHandler)
            : base(typeof(IDynamicProxy))
        {
            this.proxyTarget = proxyTarget;
            this.invocationHandler = invocationHandler;
        }

        public override ObjRef CreateObjRef(System.Type type)
        {
            throw new NotSupportedException("ObjRef for DynamicProxy isn't supported");
        }

        public bool CanCastTo(System.Type toType, object obj)
        {
            return true;
        }

        public string TypeName
        {
            get { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
            set { throw new System.NotSupportedException("TypeName for DynamicProxy isn't supported"); }
        }

        [DebuggerHidden]
        public override IMessage Invoke(IMessage message)
        {
            var methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)message);
            var method = methodMessage.MethodBase;

            object returnValue = null;
            if (method.DeclaringType == typeof(IDynamicProxy))
                returnValue = method.Invoke(this, methodMessage.Args);
            else
                returnValue = invocationHandler(proxyTarget, method, methodMessage.Args);

            return new ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount,
                methodMessage.LogicalCallContext, methodMessage);
        }

    }
}
