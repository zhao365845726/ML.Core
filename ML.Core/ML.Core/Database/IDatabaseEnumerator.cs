using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Database
{
    public interface IDatabaseEnumerator
    {
        object Current { get; }
        bool MoveNext();
        void Reset();
    }
}
