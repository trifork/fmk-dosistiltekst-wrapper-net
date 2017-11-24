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
        public SortedSet<DayWrapper> Days { get; protected set; }
        public Object refToSource { get; protected set; }
        public string dosagePeriodPostfix;
	
	    // Cached values
	    private bool? areAllDaysTheSame;
	    private bool? areAllDosesTheSame;
	    private List<DayWrapper> daysAsList;

        private class DayComparer : IComparer<DayWrapper>
        {
            int IComparer<DayWrapper>.Compare(DayWrapper d1, DayWrapper d2)
            {
                return d1.DayNumber - d2.DayNumber;
            }
        }

        static DayComparer dayComparer = new DayComparer();

	    /**
	     * Factory metod to create structured dosages
	     */
	    public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, params DayWrapper[] days) 
        {
		    SortedSet<DayWrapper> set = new SortedSet<DayWrapper>(days, dayComparer);
		    return new StructureWrapper(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, set, null);
	    }
	
	    /**
	     * Factory metod to create structured dosages
	     */
	    public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, IEnumerable<DayWrapper> days, Object refToSource) 
        {
		    return new StructureWrapper(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, (SortedSet<DayWrapper>)days, refToSource);
	    }

        public static StructureWrapper MakeStructure(int iterationInterval, String supplText, DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, IEnumerable<DayWrapper> days)
        {
		    return MakeStructure(iterationInterval, supplText, startDateOrDateTime, endDateOrDateTime, days, null);
	    }
	
	    private StructureWrapper(int iterationInterval, String supplText, 
			    DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime,
			    SortedSet<DayWrapper> days,
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


        public bool startsAndEndsSameDay()
        {
            if (StartDateOrDateTime == null || EndDateOrDateTime == null) return false;
            return StartDateOrDateTime.GetDateOrDateTime().Year == EndDateOrDateTime.GetDateOrDateTime().Year &&
                StartDateOrDateTime.GetDateOrDateTime().DayOfYear == EndDateOrDateTime.GetDateOrDateTime().DayOfYear;
        }
	
	    public List<DayWrapper> getDaysAsList() {
		    if(daysAsList==null)
			    daysAsList = new List<DayWrapper>(Days);
		    return daysAsList;
	    }
	
	    public DayWrapper getDay(int dayNumber) {
            return Days.FirstOrDefault(day => day.DayNumber == dayNumber);
	    }
	
	    public bool SameDayOfWeek() {
		     var daysAsList = getDaysAsList();
		     if(daysAsList.Count == 1)
			     return false;
		     int remainder = -1;
		     foreach(var day in  daysAsList) 
             {
			     int r = day.DayNumber % 7;
			     if(remainder >= 0 && remainder != r)
				    return false;
			     remainder = r;
		     }
		     return true;
	    }
	
	    public bool AllDaysAreTheSame() {
		    if(!areAllDaysTheSame.HasValue) {
			    areAllDaysTheSame = true;
			    DayWrapper day0 = null;
			    foreach(var day in Days) {
				    if(day0 == null) 
                    {
					    day0 = day;
				    }
				    else {
                        if (day0.NumberOfDoses != day.NumberOfDoses)
                        {
						    areAllDaysTheSame = false;
						    break;						
					    }
					    else {
						    for(int d=0; d<day0.NumberOfDoses; d++) {
							    if(!day0.GetAllDoses()[d].TheSameAs(day.GetAllDoses()[d])) {
								    areAllDaysTheSame = false;
								    break;						
							    }
						    }
					    }
				    }
			    }
		    }
		    return areAllDaysTheSame.Value;
	    }	

	    public bool DaysAreInUninteruptedSequenceFromOne() {
		    int dayNumber = 1;
		    foreach(var day in Days) {
			    if(day.DayNumber != dayNumber)
				    return false;
			    dayNumber++;
		    }
		    return true;
	    }
	
	    /**
	     * Compares dosage quantities and the dosages label (the type of the dosage)
	     * @return true if all dosages are of the same type and has the same quantity
	     */
	    public bool AllDosesAreTheSame() {
		    if(!areAllDosesTheSame.HasValue) {
			    areAllDosesTheSame = true;
			    DoseWrapper dose0 = null;
			    foreach(var day in Days) {
				    foreach(var dose in day.GetAllDoses()) {
					    if(dose0 == null) {
						    dose0 = dose;
					    }
					    else if(!dose0.TheSameAs(dose)) {
						    areAllDosesTheSame = false;
						    break;
					    }	
				    }
			    }
		    }
		    return areAllDosesTheSame.Value;
	    }
	
	    public bool ContainsMorningNoonEveningNightDoses() {
            return Days.Any( day => day.ContainsMorningNoonEveningNightDoses());
	    }	
	
	    public bool ContainsPlainDose() {		
            return Days.Any( day => day.ContainsPlainDose());
	    }	

	    public bool ContainsTimedDose() {		
            return Days.Any( day => day.ContainsTimedDose());
	    }	
	
	    public bool ContainsAccordingToNeedDosesOnly() {		
            return Days.All( day => day.ContainsAccordingToNeedDosesOnly());
	    }

	    public bool ContainsAccordingToNeedDose() {		
            return Days.Any( day => day.ContainsAccordingToNeedDose());
	    }
    }
}
