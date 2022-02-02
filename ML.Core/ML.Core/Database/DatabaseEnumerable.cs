using ML.Core.Database.Generic;
using System;

namespace ML.Core.Database
{
    public static class DatabaseEnumerable
    {
        public extern static IDatabaseEnumerable<TResult> Select<TSource, TResult>(this IDatabaseEnumerable<TSource> source, Func<TSource, TResult> selector);
        public extern static TSource ToCustomType<TSource>(this IDatabaseEnumerable<TSource> source);
    }
}
