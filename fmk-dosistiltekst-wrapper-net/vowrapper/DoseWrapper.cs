using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public abstract class DoseWrapper
    {
        // Wrapped values
        public double? MinimalDoseQuantity { get; protected set; }
        public double? MaximalDoseQuantity { get; protected set; }
        public double? DoseQuantity { get; protected set; }

        public string MinimalDoseQuantityString { get; protected set; }
        public string MaximalDoseQuantityString { get; protected set; }
        public string DoseQuantityString { get; protected set; }

        public bool IsAccordingToNeed { get; protected set; }

        protected DoseWrapper(
                double? doseQuantity, double? minimalDoseQuantity, double? maximalDoseQuantity,
                bool isAccordingToNeed)
        {
            this.DoseQuantity = doseQuantity;
            this.MinimalDoseQuantity = minimalDoseQuantity;
            this.MaximalDoseQuantity = maximalDoseQuantity;
            this.IsAccordingToNeed = isAccordingToNeed;
            if (minimalDoseQuantity.HasValue)
                MinimalDoseQuantityString = Trim(minimalDoseQuantity.Value.ToString().Replace('.', ','));
            if (maximalDoseQuantity.HasValue)
                MaximalDoseQuantityString = Trim(maximalDoseQuantity.Value.ToString().Replace('.', ','));
            if (doseQuantity.HasValue)
                DoseQuantityString = Trim(doseQuantity.Value.ToString().Replace('.', ',')).Replace(".", ",");
        }

        private static string Trim(string number)
        {
            if (number.IndexOf('.') < 0 && number.IndexOf(',') < 0)
                return number;
            if (number.Length == 1 || number[number.Length - 1] > '0')
                return number;
            else
                return Trim(number.Substring(0, number.Length - 1));
        }

        protected static bool IsZero(double? quantity)
        {
            if (!quantity.HasValue)
                return true;
            else
                return quantity.Value < 0.000000001;
        }

        protected static bool IsZero(double? minimalQuantity, double? maximalQuantity)
        {
            return !minimalQuantity.HasValue && !maximalQuantity.HasValue;
        }

        [JsonProperty(PropertyName="type")]
        public string WrapperType
        {
            get { return GetType().Name; }
        }
	
    }
}
