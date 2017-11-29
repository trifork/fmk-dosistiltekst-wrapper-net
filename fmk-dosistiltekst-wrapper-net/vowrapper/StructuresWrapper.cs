using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class StructuresWrapper
    {
        public UnitOrUnitsWrapper UnitOrUnits { get; protected set; }
        public ICollection<StructureWrapper> Structures { get; protected set; }

        public static StructuresWrapper MakeStructures(UnitOrUnitsWrapper unitOrUnits, params StructureWrapper[] structures)
        {
            var set = new LinkedList<StructureWrapper>(structures);
            return new StructuresWrapper(unitOrUnits, set);
        }

        public static StructuresWrapper MakeStructures(UnitOrUnitsWrapper unitOrUnits, ICollection<StructureWrapper> structures)
        {
            return new StructuresWrapper(unitOrUnits, structures);
        }

        private StructuresWrapper(UnitOrUnitsWrapper unitOrUnits, ICollection<StructureWrapper> structures)
        {
            this.UnitOrUnits = unitOrUnits;
            if (structures == null || structures.Count == 0)
                throw new ArgumentException();
            this.Structures = structures;
        }

        private DateTime MakeStart(DateOrDateTimeWrapper dd)
        {
            DateTime d;
            if (dd != null && dd.DateTime.HasValue)
            {
                d = dd.DateTime.Value;
            }
            else if (dd != null && dd.Date.HasValue)
            {
                d = dd.Date.Value.Date;
            }
            else
            {
                d = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            }
            return d;
        }

        private DateTime MakeEnd(DateOrDateTimeWrapper de)
        {
            DateTime d;

            if (de != null && de.DateTime.HasValue)
            {
                d = de.DateTime.Value;
            }
            else if (de != null && de.Date.HasValue)
            {
                d = de.Date.Value.Date;
            }
            else
            {
                d = new DateTime(2999, 12, 31, 23, 59, 59, 0);
            }
            return d;
        }

    }
}
