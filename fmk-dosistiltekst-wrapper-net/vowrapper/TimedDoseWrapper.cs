﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class TimedDoseWrapper: DoseWrapper
    {
        public LocalTime Time { get; protected set; }

	    private TimedDoseWrapper(
            LocalTime time,
			double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity, 
			String doseQuantityString, String minimalDoseQuantityString, String maximalDoseQuantityString, 
			bool isAccordingToNeed): base(doseQuantity, minimalDoseQuantity, maximalDoseQuantity, isAccordingToNeed) 
        {
		    this.Time = time;
        }

        public static TimedDoseWrapper MakeDose(LocalTime time, double? quantity, bool isAccordingToNeed)
        {
		    if(IsZero(quantity))
			    return null;
		    return new TimedDoseWrapper(time, quantity, null, null, null, null, null, isAccordingToNeed);
	    }

        public static TimedDoseWrapper MakeDose(LocalTime time, double? quantity, String supplText, bool isAccordingToNeed)
        {
		    if(IsZero(quantity))
			    return null;
		    return new TimedDoseWrapper(time, quantity, null, null, supplText, null, null, isAccordingToNeed);
	    }

        public static TimedDoseWrapper MakeDose(LocalTime time, double? minimalQuantity, double? maximalQuantity, bool isAccordingToNeed)
        {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new TimedDoseWrapper(time, null, minimalQuantity, maximalQuantity, null, null, null, isAccordingToNeed);
	    }

        public static TimedDoseWrapper MakeDose(LocalTime time, double? minimalQuantity, double? maximalQuantity, String minimalSupplText, String maximalSupplText, bool isAccordingToNeed)
        {
		    if(IsZero(minimalQuantity, maximalQuantity))
			    return null;
		    return new TimedDoseWrapper(time, null, minimalQuantity, maximalQuantity, null, minimalSupplText, maximalSupplText, isAccordingToNeed);
	    }

	    public string GetTime() {
            return Time.ToString();
	    }
    }
}
