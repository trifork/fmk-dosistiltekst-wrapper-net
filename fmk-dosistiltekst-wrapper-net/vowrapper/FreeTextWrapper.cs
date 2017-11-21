using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class FreeTextWrapper
    {
        public DateOrDateTimeWrapper StartDateOrDateTime { get; protected set; }
        public DateOrDateTimeWrapper EndDateOrDateTime { get; protected set; }
        public string Text { get; protected set; }

        public static FreeTextWrapper MakeFreeText(DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, string text)
        {
            return new FreeTextWrapper(startDateOrDateTime, endDateOrDateTime, text);
        }

        private FreeTextWrapper(DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime, string text)
        {
            this.StartDateOrDateTime = startDateOrDateTime;
            this.EndDateOrDateTime = endDateOrDateTime;
            this.Text = text;
        }
    }
}
