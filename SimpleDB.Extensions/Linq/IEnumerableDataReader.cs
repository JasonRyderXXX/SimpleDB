using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SimpleDB.Extensions.Linq
{

    public static class SimpleDBFluentExtensions
    {
        private static IDictionary<Type, IDictionary<string, Action<object, object>>> SetterCache = new Dictionary<Type, IDictionary<string, Action<object, object>>>();

        public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader reader)
        {
            while (reader.Read())
                yield return reader;
        }
        public static IEnumerable<KeyValuePair<string, object>> AsEnumerable(this IDataRecord record)
        {
            for (int i = 0; i < record.FieldCount; i++)
                yield return new KeyValuePair<string, object>(record.GetName(i), record[i]);
        }
        private static IDictionary<string, Action<object, object>> GetSetters(object value)=>
            value
            .GetType()
            .GetProperties()
            .Where(a => a.CanWrite)
            .ToDictionary(a => a.Name, a => new Action<object,object>(a.SetValue),StringComparer.CurrentCultureIgnoreCase);
        public static void ForEach<t>(this IEnumerable<t> items,Action<t> action)
        {
            foreach (t item in items)
                action(item);
        }
        public static t Set<t>(this t value, string propertyname, object propvalue)
        {
            var valtype = typeof(t);
            if (!SetterCache.ContainsKey(valtype))
                SetterCache.Add(valtype,GetSetters(value));
            SetterCache[valtype][propertyname](value,propvalue);
            return value;
        }
        public static t CastAs<t> (this IDataRecord record) where t:new()
        {
            var valtypeof = typeof(t);
            var ret = new t();
            record
            .AsEnumerable()
            .ForEach(a =>
            ret = ret.Set(a.Key, a.Value));
            return ret;
        }
        public static IEnumerable<t> ReadAs<t> (this IDataReader records) where t:new()
        {
            foreach(var item in records
            .AsEnumerable()
            .Select(a => a.CastAs<t>()))
                yield return item;
        }

    }
}
