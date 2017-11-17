using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class UnitOrUnitsWrapper
    {
        public string Unit { get; protected set; }
        public string UnitSingular { get; protected set; }
        public string UnitPlural { get; protected set; }

        public static UnitOrUnitsWrapper MakeUnit(string unit)
        {
            return new UnitOrUnitsWrapper(unit, null, null);
        }

        public static UnitOrUnitsWrapper MakeUnits(string unitSingular, string unitPlural)
        {
            return new UnitOrUnitsWrapper(null, unitSingular, unitPlural);
        }

        private UnitOrUnitsWrapper(string unit, string unitSingular, string unitPlural)
        {
            this.Unit = unit;
            this.UnitSingular = unitSingular;
            this.UnitPlural = unitPlural;
        }
    }
}
