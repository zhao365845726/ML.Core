using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Database.Generic
{
    public interface IDatabaseEnumerable<out T> : IDatabaseEnumerable
    {
        IDatabaseEnumerator<T> GetDatabaseEnumerator();
    }
}
