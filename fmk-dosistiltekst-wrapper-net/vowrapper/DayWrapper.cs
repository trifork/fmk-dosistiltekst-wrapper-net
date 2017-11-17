using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class DayWrapper
    {
        [JsonProperty(PropertyName="allDoses")]
	    private List<DoseWrapper> allDoses = new List<DoseWrapper>();
        [JsonProperty(PropertyName = "plainDoses")]
	    private List<PlainDoseWrapper> plainDoses = new List<PlainDoseWrapper>();
        [JsonProperty(PropertyName = "timedDoses")]
	    private List<TimedDoseWrapper> timedDoses = new List<TimedDoseWrapper>();

        public int DayNumber { get; protected set; }
        public MorningDoseWrapper MorningDose { get; protected set; }
        public NoonDoseWrapper NoonDose { get; protected set; }
        public EveningDoseWrapper EveningDose { get; protected set; }
        public NightDoseWrapper NightDose { get; protected set; }
	
	    // Helper / cached values
	    private bool? areAllDosesTheSame = new Nullable<bool>();
        private bool? areAllDosesExceptTheFirstTheSame  = new Nullable<bool>();
        private bool? areAllDosesHaveTheSameQuantity  = new Nullable<bool>();

	    private List<DoseWrapper> accordingToNeedDoses;
	
	    private DayWrapper() {
		
	    }
	
	    public static DayWrapper MakeDay(int dayNumber, params DoseWrapper[] doses) {
		    DayWrapper day = new DayWrapper();
		    day.DayNumber = dayNumber;
		    foreach(DoseWrapper dose in doses) {
			    if(dose!=null) {		
				    if(dose is PlainDoseWrapper)
					    day.plainDoses.Add((PlainDoseWrapper)dose);
				    else if(dose is TimedDoseWrapper)
					    day.timedDoses.Add((TimedDoseWrapper)dose);
				    else if(dose is MorningDoseWrapper)
					    day.MorningDose = (MorningDoseWrapper)dose;
				    else if(dose is NoonDoseWrapper)
					    day.NoonDose = (NoonDoseWrapper)dose;
				    else if(dose is EveningDoseWrapper)
					    day.EveningDose = (EveningDoseWrapper)dose;
				    else if(dose is NightDoseWrapper)
					    day.NightDose = (NightDoseWrapper)dose;
				    else 
					    throw new Exception();
				    day.allDoses.Add(dose);
			    }
		    }
		    return day;
	    }

        [JsonProperty(PropertyName = "numberOfDoses")]
	    public int NumberOfDoses
        {
            get { return allDoses.Count; }
	    }

	    public DoseWrapper GetDose(int index) {
		    return allDoses[index];		
	    }


        [JsonProperty(PropertyName = "numberOfAccordingToNeedDoses")]
	    public int NumberOfAccordingToNeedDoses 
        {
            get { return AccordingToNeedDoses.Count; }
	    }

        [JsonProperty(PropertyName = "accordingToNeedDoses")]
	    public List<DoseWrapper> AccordingToNeedDoses {
            get
            {
                // Since the 2012/06/01 namespace "according to need" is just a flag
                if (accordingToNeedDoses == null)
                {
                    accordingToNeedDoses = new List<DoseWrapper>(allDoses.Where(d => d.IsAccordingToNeed));
                }
                return accordingToNeedDoses;
            }
	    }

	
	    public List<PlainDoseWrapper> getPlainDoses() {
		    return plainDoses;
	    }
	
        [JsonProperty(PropertyName = "numberOfPlainDoses")]
	    public int NumberOfPlainDoses
        {
		    get { return plainDoses.Count; }
	    }	
	
	    public List<DoseWrapper> GetAllDoses() {
		    return allDoses;		
	    }
	
	    /**
	     * Compares dosage quantities and the dosages label (the type of the dosage)
	     * @return true if all dosages are of the same type and has the same quantity
	     */
	    public bool AllDosesAreTheSame() {
		    if(!areAllDosesTheSame.HasValue) {
			    areAllDosesTheSame = true;
			    DoseWrapper dose0 = null;
			    foreach(DoseWrapper dose in allDoses) {
				    if(dose0==null) {
					    dose0 = dose;
				    }
				    else if(!dose0.TheSameAs(dose)) {
					    areAllDosesTheSame = false;
					    break;
				    }	
			    }
		    }
		    return areAllDosesTheSame.Value;
	    }


        public bool AllDosesButTheFirstAreTheSame()
        {
		    if(!areAllDosesExceptTheFirstTheSame.HasValue) {
			    areAllDosesExceptTheFirstTheSame = true;
			    DoseWrapper dose0 = null;
			    for(int i = 1; i < NumberOfDoses;i++) {
				    if(dose0==null) {
					    dose0 = allDoses[i];
				    }
				    else if(!dose0.TheSameAs(allDoses[i])) {
					    areAllDosesExceptTheFirstTheSame = false;
					    break;
				    }	
			    }
		    }
		    return areAllDosesExceptTheFirstTheSame.Value;
	    }
	

	    /**
	     * Compares dosage quantities (but not the dosages label)
	     * @return true if all dosages has the same quantity
	     */
	    public bool AllDosesHaveTheSameQuantity() {
		    if(!areAllDosesHaveTheSameQuantity.HasValue) {
			    areAllDosesHaveTheSameQuantity = true;
			    if(allDoses.Count > 1) {
				    DoseWrapper dose0 = allDoses[0];
				    for(int i=1; i<allDoses.Count; i++) {
                        if (dose0.GetAnyDoseQuantityString() != allDoses[i].GetAnyDoseQuantityString())
                        {
						    areAllDosesHaveTheSameQuantity = false;
						    break;
					    }
				    }
			    }
		    }
		    return areAllDosesHaveTheSameQuantity.Value;
	    }
	
	    public bool ContainsAccordingToNeedDose() {
            return allDoses.Exists(d => d.IsAccordingToNeed);
	    }
	
	    public bool ContainsTimedDose() {
            return allDoses.Exists(d => d is TimedDoseWrapper);
	    }
	
	    public bool ContainsPlainDose() {
            return allDoses.Exists(d => d is PlainDoseWrapper);
	    }
	
	    public bool ContainsOnlyPNOrFixedDoses() {
		    return ContainsAccordingToNeedDosesOnly() || ContainsFixedDosesOnly();
	    }
	
	    public bool ContainsPlainNotAccordingToNeedDose() {
            return allDoses.Exists(d => d is PlainDoseWrapper && !d.IsAccordingToNeed);
	    }

	    public bool ContainsMorningNoonEveningNightDoses() {
            return allDoses.Exists(d => d is MorningDoseWrapper 
                || d is NoonDoseWrapper 
			    || d is EveningDoseWrapper 
                || d is NightDoseWrapper);
	    }

	    public bool ContainsAccordingToNeedDosesOnly() {
            return allDoses.All(d => d.IsAccordingToNeed);
	    }
	
	    public bool ContainsFixedDosesOnly() {
            return allDoses.All(d => !d.IsAccordingToNeed);
	    }
	
	    public Interval<double> GetSumOfDoses() {
		    double minValue = NewDosage();
		    double maxValue = NewDosage();
		    foreach(DoseWrapper dose in allDoses) {
			    if(dose.DoseQuantity.HasValue) {
				    minValue = minValue + dose.DoseQuantity.Value;
				    maxValue = maxValue + dose.DoseQuantity.Value;
			    }
			    else if(dose.MinimalDoseQuantity.HasValue && dose.MaximalDoseQuantity.HasValue) {
				    minValue = minValue + dose.MinimalDoseQuantity.Value;
				    maxValue = maxValue + dose.MaximalDoseQuantity.Value;
			    }
			    else {
				    throw new Exception();
			    }
		    }		
		    return new Interval<double>(minValue, maxValue);		
	    }
	
	    private static double NewDosage() {
            double v = 0;
		    // TODO:v = v.setScale(9, BigDecimal.ROUND_HALF_UP);
		    return v;
	    }

    }
}
