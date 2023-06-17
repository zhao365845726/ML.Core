using System;
using System.Collections.Generic;
using System.Text;
using ML.Core.Database;

namespace ML.Core.Database.Generic
{
    public interface IDatabaseEnumerator<out T> : IDatabaseEnumerator, IDisposable
    {
        T Current { get; }
    }
}
