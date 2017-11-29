using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
        
    public class StructureWrapper {
	
	    // Mapped values
        public int IterationInterval { get; protected set; }
        public string SupplText { get; protected set; }
        public DateOrDateTimeWrapper StartDateOrDateTime { get; protected set; }
        public DateOrDateTimeWrapper EndDateOrDateTime { get; protected set; }
        public List<DayWrapper> Days { get; protected set; }
        public Object refToSource { get; protected set; }

	    /**
	     * Factory metod to create structured dosages
	     */
	    public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, params DayWrapper[] days) 
        {
		    var set = new List<DayWrapper>(days);
		    return new StructureWrapper(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, set, null);
	    }
	
	    /**
	     * Factory metod to create structured dosages
	     */
	    public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, List<DayWrapper> days, Object refToSource) 
        {
		    return new StructureWrapper(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, days, refToSource);
	    }

        public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, List<DayWrapper> days)
        {
		    return MakeStructure(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, days, null);
	    }
	
	    private StructureWrapper(int iterationInterval, String supplText, 
			    DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime,
			    List<DayWrapper> days,
			    Object refToSource) {
		    this.IterationInterval = iterationInterval;
		    this.SupplText = supplText;
		    this.StartDateOrDateTime = startDateOrDateTime;
		    this.EndDateOrDateTime = endDateOrDateTime;
		    if(days == null)
			    throw new ArgumentException();

		    this.Days = days;
		    this.refToSource = refToSource;
	    }
    }
}
