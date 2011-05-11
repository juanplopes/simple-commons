using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simple.Reflection
{
    public static class DictionaryHelper
    {
        public static IDictionary<string, object> FromExpressions(params Expression<Func<object, object>>[] items)
        {
            return FromExpressions(null, items);
        }
        public static IDictionary<string, object> FromExpressions(IEqualityComparer<string> comparer, params Expression<Func<object, object>>[] items)
        {
            var value = new Dictionary<string, object>(comparer ?? StringComparer.InvariantCulture);
            if (items == null) return value;

            foreach (var item in items)
            {
                var name = item.Parameters[0].Name;
                value[name] = item.Compile()(null);
            }

            return value;
        }

        public static IDictionary<string, object> FromAnonymous(object obj)
        {
            return FromAnonymous(null, obj);
        }

        public static IDictionary<string, object> FromAnonymous(IEqualityComparer<string> comparer, object obj)
        {
            var value = new Dictionary<string, object>(comparer ?? StringComparer.InvariantCulture);
            if (obj == null) return value;

            foreach (var prop in obj.GetType().GetProperties())
            {
                value[prop.Name] = prop.GetValue(obj, null);
            }

            return value;
        }
    }
}
