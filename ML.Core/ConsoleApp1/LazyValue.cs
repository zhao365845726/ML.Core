using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class LazyValue<T> where T : struct
    {
        private Nullable<T> val;
        private Func<T> getValue;

        // Constructor.
        public LazyValue(Func<T> func)
        {
            val = null;
            getValue = func;
        }

        public T Value
        {
            get
            {
                if (val == null)
                    // Execute the delegate.
                    val = getValue();
                return (T)val;
            }
        }
    }
}
