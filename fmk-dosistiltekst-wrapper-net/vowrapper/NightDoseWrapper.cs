using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class NightDoseWrapper: DoseWrapper 
    {
	    private NightDoseWrapper(
			double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity, 
			string doseQuantityString, string minimalDoseQuantityString, string maximalDoseQuantityString, 
			bool isAccordingToNeed):
		    base(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) {}

	    public static NightDoseWrapper MakeDose(double quantity) {
		    if(IsZero(quantity))
			    return null;
		    return new NightDoseWrapper(quantity, null, null, null, null, null, false);
	    }

	    public static NightDoseWrapper MakeDose(double quantity, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new NightDoseWrapper(quantity, null, null, null, null, null, isAccordingToNeed);
	    }

	    public static NightDoseWrapper MakeDose(double quantity, string supplText) {
		    if(IsZero(quantity))
			    return null;
		    return new NightDoseWrapper(quantity, null, null, supplText, null, null, false);
	    }

	    public static NightDoseWrapper MakeDose(double quantity, string supplText, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new NightDoseWrapper(quantity, null, null, supplText, null, null, isAccordingToNeed);
	    }
	
	    public static NightDoseWrapper MakeDose(double minimalQuantity, double maximalQuantity) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new NightDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, false);
	    }	

	    public static NightDoseWrapper MakeDose(double minimalQuantity, double maximalQuantity, bool isAccordingToNeed) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new NightDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, isAccordingToNeed);
	    }
}

}
