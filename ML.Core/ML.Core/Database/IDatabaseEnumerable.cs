using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Database
{
    public interface IDatabaseEnumerable
    {
        IDatabaseEnumerator GetDatabaseHandle();
    }
}
