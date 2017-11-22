using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class Interval<T>
    {
        public T MinimumValue { get; protected set; }
        public T MaximumValue { get; protected set; }

        public Interval(T minimumValue, T maximumValue)
        {
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
        }

        public override string ToString()
        {
            return "[" + MinimumValue + "," + MaximumValue + "]";
        }

    }
}
