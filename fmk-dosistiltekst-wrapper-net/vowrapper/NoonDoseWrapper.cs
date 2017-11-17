using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class NoonDoseWrapper: DoseWrapper 
    {

       	public const String LABEL = "middag";  
        	
	    private NoonDoseWrapper(
			double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity, 
			string doseQuantityString, string minimalDoseQuantityString, string maximalDoseQuantityString, 
			bool isAccordingToNeed):
		    base(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) {}

	    public static NoonDoseWrapper makeDose(double quantity) {
		    if(IsZero(quantity))
			    return null;
		    return new NoonDoseWrapper(quantity, null, null, null, null, null, false);
	    }

	    public static NoonDoseWrapper makeDose(double quantity, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new NoonDoseWrapper(quantity, null, null, null, null, null, isAccordingToNeed);
	    }

	    public static NoonDoseWrapper makeDose(double quantity, string supplText) {
		    if(IsZero(quantity))
			    return null;
		    return new NoonDoseWrapper(quantity, null, null, supplText, null, null, false);
	    }

	    public static NoonDoseWrapper makeDose(double quantity, string supplText, bool isAccordingToNeed) {
		    if(IsZero(quantity))
			    return null;
		    return new NoonDoseWrapper(quantity, null, null, supplText, null, null, isAccordingToNeed);
	    }
	
	    public static NoonDoseWrapper makeDose(double minimalQuantity, double maximalQuantity) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new NoonDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, false);
	    }	

	    public static NoonDoseWrapper makeDose(double minimalQuantity, double maximalQuantity, bool isAccordingToNeed) {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new NoonDoseWrapper(null, minimalQuantity, maximalQuantity, null, null, null, isAccordingToNeed);
	    }

        public override string Label
        {
            get { return LABEL; }
        }
}

}
