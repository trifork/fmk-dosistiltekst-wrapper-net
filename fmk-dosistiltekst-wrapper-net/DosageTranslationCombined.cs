using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public class DosageTranslationCombined
    {
        public DosageTranslation CombinedTranslation { get; protected set; }
        public List<DosageTranslation> PeriodTranslations { get; protected set; }

        public DosageTranslationCombined(DosageTranslation combinedTranslation, List<DosageTranslation> periodTranslations)
        {
            CombinedTranslation = combinedTranslation;
            PeriodTranslations = periodTranslations;
        }
    }
}
