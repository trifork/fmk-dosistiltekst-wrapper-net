using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class AdministrationAccordingToSchemaWrapper
    {
        public DateOrDateTimeWrapper StartDateOrDateTime { get; protected set; }
        public DateOrDateTimeWrapper EndDateOrDateTime { get; protected set; }

        public static AdministrationAccordingToSchemaWrapper makeAdministrationAccordingToSchema(DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime)
        {
            return new AdministrationAccordingToSchemaWrapper(startDateOrDateTime, endDateOrDateTime);
        }

        private AdministrationAccordingToSchemaWrapper(DateOrDateTimeWrapper startDateOrDateTime, DateOrDateTimeWrapper endDateOrDateTime)
        {
            this.StartDateOrDateTime = startDateOrDateTime;
            this.EndDateOrDateTime = endDateOrDateTime;
        }
    }
}
