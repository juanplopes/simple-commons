using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.DynamicProxy
{
    public class EasyProxy
    {
        InvocationDelegate handler;
        public EasyProxy(InvocationDelegate handler)
        {
            this.handler = handler;
        }

        public object Create(object target)
        {
            return CreateProxy(target).GetTransparentProxy();
        }

        public object CreateMarshallable(MarshalByRefObject target)
        {
            return CreateMarshallableProxy(target).GetTransparentProxy();
        }

        public IDynamicProxy CreateProxy(object target)
        {
            return new InterfaceProxy(target, handler);
        }

        public IDynamicProxy CreateMarshallableProxy(MarshalByRefObject target)
        {
            return new MarshallableProxy(target, handler);
        }
    }
}
