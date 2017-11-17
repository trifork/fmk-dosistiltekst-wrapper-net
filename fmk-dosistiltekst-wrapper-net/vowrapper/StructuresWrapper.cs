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
        public SortedSet<StructureWrapper> Structures { get; protected set; }

        private static IComparer<StructureWrapper> structureComparer = Comparer<StructureWrapper>.Create((o1, o2) =>
        {
            int i = o1.StartDateOrDateTime.GetDateOrDateTime().CompareTo(o2.StartDateOrDateTime.GetDateOrDateTime());
            if (i != 0)
                return i;
            if (o1.ContainsAccordingToNeedDosesOnly())
                return 1;
            else
                return -1;
        });


        public static StructuresWrapper MakeStructures(UnitOrUnitsWrapper unitOrUnits, params StructureWrapper[] structures)
        {
            var set = new SortedSet<StructureWrapper>(structures, structureComparer);
            return new StructuresWrapper(unitOrUnits, set);
        }

        public static StructuresWrapper MakeStructures(UnitOrUnitsWrapper unitOrUnits, IEnumerable<StructureWrapper> structures)
        {
            return new StructuresWrapper(unitOrUnits, (SortedSet<StructureWrapper>)structures);
        }

        private StructuresWrapper(UnitOrUnitsWrapper unitOrUnits, SortedSet<StructureWrapper> structures)
        {
            this.UnitOrUnits = unitOrUnits;
            if (structures == null || structures.Count == 0)
                throw new ArgumentException();
            this.Structures = structures;
        }

        public bool HasOverlappingPeriodes()
        {
            var ss = new List<StructureWrapper>(Structures);
            for (int i = 0; i < ss.Count; i++)
            {
                for (int j = i + 1; j < ss.Count; j++)
                {
                    var dis = ss[i].StartDateOrDateTime;
                    var die = ss[i].EndDateOrDateTime;
                    var djs = ss[j].StartDateOrDateTime;
                    var dje = ss[j].EndDateOrDateTime;
                    if (Overlaps(dis, die, djs, dje))
                        return true;
                }
            }
            return false;
        }

        private bool Overlaps(DateOrDateTimeWrapper dd1Start, DateOrDateTimeWrapper dd1End, DateOrDateTimeWrapper dd2Start, DateOrDateTimeWrapper dd2End)
        {
            var dt1Start = MakeStart(dd1Start);
            var dt2Start = MakeStart(dd2Start);
            if (dt1Start == dt2Start)
            {
                return true;
            }
            var dt1End = MakeEnd(dd1End);
            var dt2End = MakeEnd(dd2End);
            if (dt1Start < dt2Start)
            {
                return dt1End > dt2End;
            }
            else
            {
                return dt2End > dt1End;
            }
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
