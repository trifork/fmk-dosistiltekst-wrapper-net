using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class TimedDoseWrapper: DoseWrapper
    {
   	    public static string LABEL = "kl.";  

        private DateTime time;

	    private TimedDoseWrapper(
            DateTime time,
			double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity, 
			String doseQuantityString, String minimalDoseQuantityString, String maximalDoseQuantityString, 
			bool isAccordingToNeed): base(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) 
        {
		    this.time = time;
        }

	    public static TimedDoseWrapper makeDose(DateTime time, double? quantity, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new TimedDoseWrapper(time, quantity, null, null, null, null, null, isAccordingToNeed);
	    }

	    public static TimedDoseWrapper makeDose(DateTime time, double? quantity, String supplText, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new TimedDoseWrapper(time, quantity, null, null, supplText, null, null, isAccordingToNeed);
	    }
	
	    public static TimedDoseWrapper makeDose(DateTime time, double? minimalQuantity, double? maximalQuantity, bool isAccordingToNeed) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new TimedDoseWrapper(time, null, minimalQuantity, maximalQuantity, null, null, null, isAccordingToNeed);
	    }	

	    public static TimedDoseWrapper makeDose(DateTime time, double? minimalQuantity, double? maximalQuantity, String minimalSupplText, String maximalSupplText, bool isAccordingToNeed) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new TimedDoseWrapper(time, null, minimalQuantity, maximalQuantity, null, minimalSupplText, maximalSupplText, isAccordingToNeed);
	    }

        public override string Label
        {
            get { return LABEL; }
        }

	    public string GetTime() {
		    return time.ToString("HH:mm:ss");
	    }

        public override bool TheSameAs(DoseWrapper other)
        {
		    if(!(other is TimedDoseWrapper))
			    return false;
		    if(!base.TheSameAs(other))
			    return false;
		    return GetTime() == ((TimedDoseWrapper)other).GetTime();
	    }
    }
}
