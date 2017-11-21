using fmk_dosistiltekst_wrapper_net.vowrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public class DailyDosis
    {
        public double? Value { get; protected set; }
        public Interval<double> Interval { get; protected set; }
        public UnitOrUnitsWrapper UnitOrUnits { get; protected set; }

        public DailyDosis()
        {
            Value = null;
            Interval = null;
        }

        public DailyDosis(double value, UnitOrUnitsWrapper unitOrUnits)
        {
            Value = value;
            UnitOrUnits = unitOrUnits;
        }

        public DailyDosis(Interval<double> interval, UnitOrUnitsWrapper unitOrUnits)
        {
            Interval = interval;
            UnitOrUnits = unitOrUnits;
        }

        public Boolean IsValue()
        {
            return Value != null;
        }

        public bool IsInterval()
        {
            return Interval != null;
        }

        public bool IsNone()
        {
            return !Value.HasValue && Interval == null;
        }

    }
}
