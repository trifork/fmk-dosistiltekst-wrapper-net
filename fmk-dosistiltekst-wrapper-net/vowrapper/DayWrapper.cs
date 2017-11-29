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

        [JsonProperty(PropertyName = "numberOfPlainDoses")]
	    public int NumberOfPlainDoses
        {
		    get { return plainDoses.Count; }
	    }	
    }
}
