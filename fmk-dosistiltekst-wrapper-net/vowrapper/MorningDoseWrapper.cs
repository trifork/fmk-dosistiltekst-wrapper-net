using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class MorningDoseWrapper: DoseWrapper 
    {
	    private MorningDoseWrapper(
			double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity, 
			string doseQuantityString, string minimalDoseQuantityString, string maximalDoseQuantityString, 
			bool isAccordingToNeed):
		    base(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) {}

	    public static MorningDoseWrapper MakeDose(double quantity) {
		    if(IsZero(quantity))
			    return null;
		    return new MorningDoseWrapper(quantity, null, null, null, null, null, false);
	    }

	    public static MorningDoseWrapper MakeDose(double quantity, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new MorningDoseWrapper(quantity, null, null, null, null, null, isAccordingToNeed);
	    }

	    public static MorningDoseWrapper MakeDose(double quantity, string supplText) {
		    if(IsZero(quantity))
			    return null;
		    return new MorningDoseWrapper(quantity, null, null, supplText, null, null, false);
	    }

	    public static MorningDoseWrapper MakeDose(double quantity, string supplText, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new MorningDoseWrapper(quantity, null, null, supplText, null, null, isAccordingToNeed);
	    }
	
	    public static MorningDoseWrapper MakeDose(double minimalQuantity, double maximalQuantity) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new MorningDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, false);
	    }	

	    public static MorningDoseWrapper MakeDose(double minimalQuantity, double maximalQuantity, bool isAccordingToNeed) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new MorningDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, isAccordingToNeed);
	    }	
    }
}
