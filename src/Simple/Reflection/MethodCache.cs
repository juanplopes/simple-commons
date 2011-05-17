using System.Collections.Generic;
using System.Reflection;
using Simple.Patterns;
using System;
using System.Linq;

namespace Simple.Reflection
{
    public class MethodCache
    {
        DelegateFactory factory = new DelegateFactory();
        Dictionary<int, InvocationDelegate> _methods = new Dictionary<int, InvocationDelegate>();

        public InvocationDelegate GetInvoker(MethodBase method)
        {
            if (method == null) throw new ArgumentNullException("method");

            InvocationDelegate res;

            var token = method.MetadataToken;
            if (!_methods.TryGetValue(token, out res))
                _methods[token] = res = factory.Create(method);

            return res;
        }

        public T CreateInstance<T>(params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        public T CreateInstance<T>(BindingFlags? flags, params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), flags, parameters);
        }

        public object CreateInstance(Type type, params object[] parameters)
        {
            return CreateInstance(type, null, parameters);
        }

        public object CreateInstance(Type type, BindingFlags? flags, params object[] parameters)
        {
            var method = FindConstructor(type, flags, ref parameters);
            if (method == null) throw new ArgumentException("the type '{0}' doesn't have the appropriate constructor");
            return GetInvoker(method)(null, parameters);
        }

        public InvocationDelegate GetConstructor(Type type, params Type[] argTypes)
        {
            return GetInvoker(type.GetConstructor(argTypes));
        }

        private static ConstructorInfo FindConstructor(Type type, BindingFlags? flags, ref object[] parameters)
        {
            var ctors = flags != null ? type.GetConstructors(flags.Value) : type.GetConstructors();
            object state;
            var method = Type.DefaultBinder.BindToMethod(
                flags ?? BindingFlags.Default, ctors, ref parameters,
                null, null, null, out state) as ConstructorInfo;
            return method;
        }

        public InvocationDelegate GetGetter(PropertyInfo prop)
        {
            return GetInvoker(prop.GetGetMethod(true));
        }

        public InvocationDelegate GetSetter(PropertyInfo prop)
        {
            return GetInvoker(prop.GetSetMethod(true));
        }
    }
}
