using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net
{
    public class DosageTranslation
    {
        public string ShortText { get; protected set; }
        public string LongText { get; protected set; }
        public DailyDosis DailyDosis { get; protected set; }

        public DosageTranslation(String shortText, String longText, DailyDosis dailyDosis)
        {
            ShortText = shortText;
            LongText = longText;
            DailyDosis = dailyDosis;
        }

    }
}
